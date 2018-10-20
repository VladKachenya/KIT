using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;
using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.InformationModel.Presentation.Interfaces;

namespace BISC.Modules.InformationModel.Presentation.Behaviors
{
    /// <summary>
    /// поведение для ToggleButton, добавляет и удаляет строки в гриде
    /// </summary>
    public class TreeViewToggleButtonBehavior : Behavior<ToggleButton>
    {
        private ToggleButton _assToggleButton;



        protected override void OnAttached()
        {

            _assToggleButton = AssociatedObject;
            if (!(_assToggleButton.DataContext is IInfoModelItemViewModel)) return;
            if ((_assToggleButton.DataContext as IInfoModelItemViewModel).ChildInfoModelItemViewModels.Count == 0)
            {
                _assToggleButton.Visibility = Visibility.Hidden;
            }
            (_assToggleButton.DataContext as IInfoModelItemViewModel).Checked += TreeGridItemCheched;
            if (_assToggleButton.IsLoaded)
            {
                _assToggleButton.Checked += TreeViewToggleButtonBehavior_Checked;
                _assToggleButton.Unchecked += TreeViewToggleButtonBehavior_Unchecked;
            }

            _assToggleButton.Unloaded += _assToggleButton_Unloaded;
            _assToggleButton.Loaded += _assToggleButton_Loaded;
            if (_assToggleButton.DataContext is IInfoModelItemViewModel)
            {
                if ((_assToggleButton.DataContext as IInfoModelItemViewModel).IsChecked)
                {
                    TreeViewToggleButtonBehavior_Checked(_assToggleButton, null);
                    _assToggleButton.IsChecked = true;
                }
            }
            base.OnAttached();
        }

        private void _assToggleButton_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _assToggleButton.Checked += TreeViewToggleButtonBehavior_Checked;
            _assToggleButton.Unchecked += TreeViewToggleButtonBehavior_Unchecked;
        }



        //OnDetaching не срабатывает, поэтому подпись на событие кнопки
        private void _assToggleButton_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _assToggleButton.Checked -= TreeViewToggleButtonBehavior_Checked;
            _assToggleButton.Unchecked -= TreeViewToggleButtonBehavior_Unchecked;
        }




        private void TreeGridItemCheched(bool? isToExpand)
        {
            if (!_assToggleButton.IsChecked.HasValue) return;
            if (isToExpand.HasValue)
            {
                if (isToExpand.Value)
                {
                    if (_assToggleButton.IsChecked.Value)
                    {
                        RefreshToggleButtonRows();
                    }

                }
                _assToggleButton.IsChecked = isToExpand;
            }
            else
            {
                _assToggleButton.IsChecked = !_assToggleButton.IsChecked.Value;
            }
        }




        private void RefreshToggleButtonRows()
        {
            ObservableCollection<IInfoModelItemViewModel> allItems =
                _assToggleButton.Tag as ObservableCollection<IInfoModelItemViewModel>;
            IInfoModelItemViewModel currentItem = _assToggleButton.DataContext as IInfoModelItemViewModel;
            if (allItems != null)
            {
                int startIndex =
                    allItems.IndexOf(currentItem);
                if (startIndex < 0) return;
                if (currentItem.ChildInfoModelItemViewModels is IEnumerable<IInfoModelItemViewModel>)
                {
                    IEnumerable<IInfoModelItemViewModel> childItems =
                        currentItem.ChildInfoModelItemViewModels;
                    foreach (var child in childItems)
                    {
                        child.Checked?.Invoke(false);
                        startIndex++;
                        if (allItems.Count > startIndex)
                        {
                            if (allItems[startIndex].GetType() == child.GetType())
                            {
                                if (allItems[startIndex] == child) continue;
                                else
                                {

                                    if (allItems.Count > startIndex)
                                    {
                                        if (allItems[startIndex].GetType() == allItems[startIndex + 1].GetType())
                                            allItems.Move(startIndex, startIndex + 1);
                                    }

                                }
                            }
                            else
                            {
                                allItems.Insert(startIndex, child);
                            }

                        }
                        else
                        {
                            allItems.Add(child);
                        }
                    }
                }
            }
        }





        private void TreeViewToggleButtonBehavior_Unchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            ObservableCollection<IInfoModelItemViewModel> allItems =
                (sender as ToggleButton).Tag as ObservableCollection<IInfoModelItemViewModel>;
            if (allItems == null) return;
             




            IInfoModelItemViewModel oldItem = ((sender as ToggleButton).DataContext as IInfoModelItemViewModel);
            if (!oldItem.ChildInfoModelItemViewModels.Any()) return;

            StaticContainer.CurrentContainer.ResolveType<ILoggingService>().LogUserAction($"Пользователь свернул узел {oldItem.Header}");

            if (oldItem == null) return;
            oldItem.IsChecked = false;
            DeleteTreeGridItem(allItems, oldItem, false);
        }

        private void TreeViewToggleButtonBehavior_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            IInfoModelItemViewModel oldItem = ((sender as ToggleButton).DataContext as IInfoModelItemViewModel);
            if(!oldItem.ChildInfoModelItemViewModels.Any())return;
            StaticContainer.CurrentContainer.ResolveType<ILoggingService>().LogUserAction($"Пользователь развернул узел {oldItem.Header}");
            if (oldItem == null) return;
            oldItem.IsChecked = true;
            ObservableCollection<IInfoModelItemViewModel> treeGridItems = ((sender as ToggleButton)?.Tag as ObservableCollection<IInfoModelItemViewModel>);
            int index = treeGridItems.IndexOf(oldItem);
            if (oldItem?.ChildInfoModelItemViewModels is IEnumerable<IInfoModelItemViewModel>)
            {
                IEnumerable<IInfoModelItemViewModel> newItems = oldItem.ChildInfoModelItemViewModels;
                foreach (IInfoModelItemViewModel newItem in newItems)
                {
                    bool isAdded = false;
                    if (newItem is IGroupedConfigurationItemViewModel)
                    {
                        if (!(newItem as IGroupedConfigurationItemViewModel).IsGroupedProperty)
                        {
                            foreach (var childInfoModelItemViewModel in newItem.ChildInfoModelItemViewModels)
                            {
                                index++;
                                AddTreeGridItem(treeGridItems, childInfoModelItemViewModel, oldItem, index);
                            }
                            isAdded = true;
                        }
                    }

                    if (!isAdded)
                    {
                        index++;
                        AddTreeGridItem(treeGridItems, newItem, oldItem, index);
                    }
                }

                return;
            }
        }



        private void AddTreeGridItem(ObservableCollection<IInfoModelItemViewModel> treeGridItems,
            IInfoModelItemViewModel newConfigurationItem,
            IInfoModelItemViewModel oldConfigurationItem, int index)
        {
            newConfigurationItem.Level = oldConfigurationItem.Level + 1;
            treeGridItems.Insert(index, newConfigurationItem);
        }

        /// <summary>
        /// рекурсивное удаление всей ветки
        /// </summary>
        /// <param name="treeGridItems">коллекция всех объектов в строках</param>
        /// <param name="removingItem">текущие элемент</param>
        /// <param name="removeFromCollection">удалять ли из коллекции текущий элемент</param>
        private void DeleteTreeGridItem(ObservableCollection<IInfoModelItemViewModel> treeGridItems,
            IInfoModelItemViewModel removingItem, bool removeFromCollection)
        {
            if (removingItem.ChildInfoModelItemViewModels is IEnumerable<IInfoModelItemViewModel>)
            {
                IEnumerable<IInfoModelItemViewModel> newItems =
                    removingItem.ChildInfoModelItemViewModels;
                foreach (IInfoModelItemViewModel newItem in newItems)
                {
                    DeleteTreeGridItem(treeGridItems, newItem, true);
                }
            }
            else if (removingItem.ChildInfoModelItemViewModels is IInfoModelItemViewModel)
            {
                DeleteTreeGridItem(treeGridItems, removingItem.ChildInfoModelItemViewModels as IInfoModelItemViewModel, true);

            }
            if (removeFromCollection) RemoveItemFromList(treeGridItems, removingItem);


        }

        private void RemoveItemFromList(ObservableCollection<IInfoModelItemViewModel> treeGridItems,
            IInfoModelItemViewModel removingItem)
        {
            treeGridItems.Remove(removingItem);
            removingItem.Level = 0;
        }
    }
}