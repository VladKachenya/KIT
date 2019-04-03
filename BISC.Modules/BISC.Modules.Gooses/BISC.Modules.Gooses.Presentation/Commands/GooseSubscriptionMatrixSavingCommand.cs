using BISC.Infrastructure.Global.Common;
using BISC.Modules.Device.Infrastructure.Model;
using BISC.Modules.Gooses.Infrastructure.Model.Matrix;
using BISC.Modules.Gooses.Model.Model;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using BISC.Presentation.Infrastructure.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Infrastructure.Services;

namespace BISC.Modules.Gooses.Presentation.Commands
{
    public class GooseSubscriptionMatrixSavingCommand : ISavingCommand
    {
        private readonly IGooseSavingService _gooseSavingService;
        private IDevice _device;
        private List<GooseControlBlockViewModel> _gooseControlBlockViewModels;
        private Func<bool> _getIsFtpSavingNeed;

        public GooseSubscriptionMatrixSavingCommand(IGooseSavingService gooseSavingService)
        {
            _gooseSavingService = gooseSavingService;
        }


        public void Initialize(IDevice device,
            List<GooseControlBlockViewModel> gooseControlBlockViewModels, Func<bool> getIsFtpSavingNeed)
        {
            _device = device;
            _gooseControlBlockViewModels = gooseControlBlockViewModels;
            _getIsFtpSavingNeed = getIsFtpSavingNeed;
        }

        public async Task<OperationResult<SavingCommandResultEnum>> SaveAsync()
        {
            List<Tuple<string, IGooseRowFtpEntity>> entitysForSaving = new List<Tuple<string, IGooseRowFtpEntity>>();
            // Перебираем гусы
            foreach (var gooseControlBlockViewModel in _gooseControlBlockViewModels)
            {
                var gooseSdRef = gooseControlBlockViewModel.GoCbReference;
                // Перебираем строчки
                foreach (var row in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    var value = row.SelectableValueViewModels.FirstOrDefault(el => el.SelectedValue);

                    if (value != null)
                    {
                        IGooseRowFtpEntity entityForSaving;
                        if (row.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.StateKey)
                        {
                            // Формируем state
                            entityForSaving = new GooseRowFtpEntity()
                            {
                                NumberOfFcdaInDataSetOfGoose = row.NumberOfFcdaInDataSet + 1,
                                BitIndex = value.ColumnNumber + 1
                            };
                        }
                        else if(row.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.QualityKey)
                        {
                            // Формируем quality
                            entityForSaving = new GooseRowQualityFtpEntity()
                            {
                                NumberOfFcdaInDataSetOfGoose = row.NumberOfFcdaInDataSet + 1,
                                BitIndex = value.ColumnNumber + 1,
                                // Устанавливаем валидити
                                IsValiditySelected = gooseControlBlockViewModel.GooseRowViewModels.First(el =>
                                        el.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.ValidityKey)
                                    .SelectableValueViewModels[value.ColumnNumber].SelectedValue
                        };
                        }
                        else
                        {
                            continue;
                        }
                        entitysForSaving.Add(new Tuple<string, IGooseRowFtpEntity>(gooseSdRef, entityForSaving));
                    }
                }
            }

            await _gooseSavingService.SaveSubscriptionMatrix(_device, entitysForSaving);

            return new OperationResult<SavingCommandResultEnum>("goven");
        }

        public async Task<bool> IsSavingByFtpNeeded()
        {
            return _getIsFtpSavingNeed.Invoke();
        }

        public async Task<OperationResult> ValidateBeforeSave()
        {
            return OperationResult.SucceedResult;
        }
    }
}