using BISC.Infrastructure.Global.IoC;
using BISC.Infrastructure.Global.Services;
using BISC.Modules.Gooses.Infrastructure.Keys;
using BISC.Modules.Gooses.Presentation.Events;
using BISC.Modules.Gooses.Presentation.ViewModels.Matrix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace BISC.Modules.Gooses.Presentation.Views.UserControl
{
    /// <summary>
    /// Interaction logic for GooseGrid.xaml
    /// </summary>
    public partial class GooseGrid : System.Windows.Controls.UserControl, IDisposable
    {
        private readonly int CellSize = 20;
        private List<TextBlock> _goinNumTextBlocks;
        private TextBlock _currentFocusedGoInNumberTextBlock;
        private List<TextBlock> _goinSignatureTextBlocks;
        private TextBlock _currentFocusedGoInSignatureTextBlock;
        private Brush _highlightBrush = new SolidColorBrush(Color.FromArgb(150, 255, 0xF5, 0x50));
        private bool _isInitialized = false;
        public GooseGrid()
        {
            InitializeComponent();
            _globalEventsService = StaticContainer.CurrentContainer.ResolveType<IGlobalEventsService>();
        }

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(GooseGrid), new PropertyMetadata(false, OnActiveChanged));

        private static void OnActiveChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d.GetValue(IsActiveProperty).Equals(true))
            {
                (d as GooseGrid).GooseGrid_Loaded(null, null);
            }
            else
            {
                (d as GooseGrid).GooseGrid_Unloaded(null, null);
            }
        }


        private void GooseGrid_Unloaded(object sender, RoutedEventArgs e)
        {
            if (_isInitialized)
            {
                _globalEventsService.Unsubscribe<SelectableBoxEventArgs>((OnSelectableBoxFocused));
            }
        }

        private void GooseGrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_isInitialized)
            {
                _globalEventsService.Subscribe<SelectableBoxEventArgs>((OnSelectableBoxFocused));
            }
        }

        private void OnSelectableBoxFocused(SelectableBoxEventArgs selectableBoxEventArgs)
        {
            if (!selectableBoxEventArgs.IsFocused)
            {
                return;
            }

            if (_currentFocusedGoInNumberTextBlock != null)
            {
                DeHighlightTextBlock(_currentFocusedGoInNumberTextBlock);
            }
            if (_goinNumTextBlocks.Count <= selectableBoxEventArgs.SelectableValueViewModel.ColumnNumber)
            {
                return;
            }

            _currentFocusedGoInNumberTextBlock = _goinNumTextBlocks[selectableBoxEventArgs.SelectableValueViewModel.ColumnNumber];
            HighlightTextBlock(_currentFocusedGoInNumberTextBlock);

            if (_currentFocusedGoInSignatureTextBlock != null)
            {
                DeHighlightTextBlock(_currentFocusedGoInSignatureTextBlock);
            }
            if (_goinSignatureTextBlocks == null)
            {
                return;
            }

            _currentFocusedGoInSignatureTextBlock = _goinSignatureTextBlocks.FirstOrDefault((block => block.Text == selectableBoxEventArgs?.SelectableValueViewModel?.Parent?.RowName));
            if (_currentFocusedGoInSignatureTextBlock == null)
            {
                return;
            }

            HighlightTextBlock(_currentFocusedGoInSignatureTextBlock);
        }

        private void HighlightTextBlock(TextBlock textBlock)
        {
            textBlock.Background = _highlightBrush;
        }

        private void DeHighlightTextBlock(TextBlock textBlock)
        {
            textBlock.Background = Brushes.White;
        }

        public ObservableCollection<GooseControlBlockViewModel> GooseControlBlockViewModels
        {
            get
            {
                return (ObservableCollection<GooseControlBlockViewModel>)GetValue(
                    GooseControlBlockViewModelsProperty);
            }
            set { SetValue(GooseControlBlockViewModelsProperty, value); }
        }

        public static readonly DependencyProperty GooseControlBlockViewModelsProperty =
            DependencyProperty.Register("GooseControlBlockViewModels",
                typeof(ObservableCollection<GooseControlBlockViewModel>),
                typeof(GooseGrid), new PropertyMetadata(null, OnGooseControlBlockViewModelsPropertyChanged));

        public int GoInCount
        {
            get { return (int)GetValue(GoInCountProperty); }
            set { SetValue(GoInCountProperty, value); }
        }

        public static readonly DependencyProperty GoInCountProperty = DependencyProperty.Register("GoInCount",
            typeof(int), typeof(GooseGrid), new PropertyMetadata(0, OnGoInCountPropertyChanged));

        private IGlobalEventsService _globalEventsService;

        private static void OnGoInCountPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var gooseGrid = d as GooseGrid;
            //gooseGrid?.OnGoInCountChanged();
        }

        private void OnGoInCountChanged()
        {
            _goinNumTextBlocks = new List<TextBlock>();
            var nameList = GooseControlBlockViewModels.FirstOrDefault()?.ColumnsName;
            if (nameList == null)
            {
                return;
            }

            foreach (var name in nameList)
            {
                TextBlock goinNumTextBlock = new TextBlock();
                goinNumTextBlock.Text = name;
                goinNumTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                goinNumTextBlock.VerticalAlignment = VerticalAlignment.Top;
                goinNumTextBlock.Padding = new Thickness(2);
                _goinNumTextBlocks.Add(goinNumTextBlock);
            }
            //for (int i = 0; i < GoInCount; i++)
            //{
            //    TextBlock goinNumTextBlock = new TextBlock();
            //    goinNumTextBlock.Text = (i + 1).ToString();
            //    goinNumTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            //    goinNumTextBlock.VerticalAlignment = VerticalAlignment.Top;
            //    goinNumTextBlock.Padding = new Thickness(2);
            //    _goinNumTextBlocks.Add(goinNumTextBlock);
            //}
        }


        private static void OnGooseControlBlockViewModelsPropertyChanged(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            var gooseGrid = d as GooseGrid;
            gooseGrid?.OnGooseControlBlockViewModelsChanged();
        }

        private void OnGooseControlBlockViewModelsChanged()
        {
            OnGoInCountChanged();
            GooseControlBlockViewModels.CollectionChanged += GooseControlBlockViewModels_CollectionChanged;
            GooseControlBlockViewModels_CollectionChanged(null, null);
        }

        private void GooseControlBlockViewModels_CollectionChanged(object sender,
            System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (GoInCount == 0)
            {
                return;
            }


            mainGrid.Children.Clear();
            mainGrid.ColumnDefinitions.Clear();
            mainGrid.RowDefinitions.Clear();
            int mainGridRowIndex = 0;
            mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) }); // номера GoIn

            for (int i = 0; i < GoInCount; i++)
            {
                TextBlock goinNumTextBlock = _goinNumTextBlocks[i];

                goinNumTextBlock.SetValue(Grid.ColumnProperty, i + 1);
                goinNumTextBlock.SetValue(Grid.RowProperty, mainGridRowIndex);
                mainGrid.Children.Add(goinNumTextBlock);
            }
            mainGridRowIndex++;
            _goinSignatureTextBlocks = new List<TextBlock>();

            ScrollViewer scrollViewer = new ScrollViewer();

            Grid scrollViewerGrid = new Grid();
            mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto }); //левая шапка для названий

            for (int i = 0; i < GoInCount; i++)
            {
                scrollViewerGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth = CellSize }); //new GridLength(CellSize)
                mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth = CellSize }); // new GridLength(CellSize)

            }

            int scrollViewerGridRowIndex = 0;
            foreach (var gooseControlBlockViewModel in GooseControlBlockViewModels)
            {

                // if (!gooseControlBlockViewModel.IsReferenceEnabled) continue;
                mainGrid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(CellSize + 35)
                }); //первая шапка

                scrollViewerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize + 35) });


                Grid headerGrid = new Grid();
                headerGrid.RowDefinitions.Add(new RowDefinition());
                headerGrid.RowDefinitions.Add(new RowDefinition());

                TextBlock gooseControlHeaderTextBlock = new TextBlock();
                gooseControlHeaderTextBlock.Text = gooseControlBlockViewModel.AppId;
                gooseControlHeaderTextBlock.FontSize += 5;

                TextBlock stateTextBlock = new TextBlock();
                stateTextBlock.Text = "GoIn State";

                gooseControlHeaderTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                gooseControlHeaderTextBlock.VerticalAlignment = VerticalAlignment.Stretch;
                stateTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                stateTextBlock.VerticalAlignment = VerticalAlignment.Stretch;
                gooseControlHeaderTextBlock.SetValue(Grid.RowProperty, 0);
                stateTextBlock.SetValue(Grid.RowProperty, 1);
                headerGrid.Children.Add(gooseControlHeaderTextBlock);
                headerGrid.Children.Add(stateTextBlock);

                headerGrid.SetValue(Grid.ColumnProperty, 0);
                headerGrid.SetValue(Grid.RowProperty, scrollViewerGridRowIndex);
                headerGrid.SetValue(Grid.ColumnSpanProperty, GoInCount);
                scrollViewerGrid.Children.Add(headerGrid);

                mainGridRowIndex++;
                scrollViewerGridRowIndex++;

                foreach (var gooseRow in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if (gooseRow.GooseRowType == "State")
                    {
                        mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });
                        scrollViewerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });

                        TextBlock stateRowNameTextBlock = new TextBlock();
                        stateRowNameTextBlock.Text = gooseRow.RowName;
                        stateRowNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        stateRowNameTextBlock.SetValue(Grid.ColumnProperty, 0);
                        stateRowNameTextBlock.SetValue(Grid.RowProperty,
                            mainGridRowIndex); //добавить в главную гриду (фиксированную)
                        mainGrid.Children.Add(stateRowNameTextBlock);
                        _goinSignatureTextBlocks.Add(stateRowNameTextBlock);
                        mainGridRowIndex++;


                        for (int i = 0; i < GoInCount; i++)
                        {
                            SelectableBox stateSelectableBox = new SelectableBox(); //добавить в гриду скроллвьюера
                            stateSelectableBox.SetValue(Grid.RowProperty, scrollViewerGridRowIndex);
                            stateSelectableBox.SetValue(Grid.ColumnProperty, i);
                            stateSelectableBox.DataContext = gooseRow.SelectableValueViewModels[i];
                            scrollViewerGrid.Children.Add(stateSelectableBox);
                        }

                        scrollViewerGridRowIndex++;
                    }
                }
                if (gooseControlBlockViewModel.GooseRowViewModels.Any(g =>
                    g.GooseRowType == GooseKeys.GooseSubscriptionPresentationKeys.Validity))
                {
                    mainGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(CellSize + 10)
                    }); //вторая шапка
                    scrollViewerGrid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new GridLength(CellSize + 10)
                    }); //вторая шапка

                    TextBlock upper2TextBlock = new TextBlock();
                    upper2TextBlock.Text = "GoIn Validity";
                    upper2TextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                    upper2TextBlock.VerticalAlignment = VerticalAlignment.Bottom;

                    upper2TextBlock.SetValue(Grid.ColumnProperty, 0);
                    upper2TextBlock.SetValue(Grid.RowProperty, scrollViewerGridRowIndex);
                    upper2TextBlock.SetValue(Grid.ColumnSpanProperty, GoInCount);
                    scrollViewerGrid.Children.Add(upper2TextBlock);
                    mainGridRowIndex++;
                    scrollViewerGridRowIndex++;
                }

                foreach (var gooseRow in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if ((gooseRow.GooseRowType == "Quality"))
                    {
                        mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });
                        scrollViewerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });
                        TextBlock qualityRowNameTextBlock = new TextBlock();
                        qualityRowNameTextBlock.Text = gooseRow.RowName;
                        qualityRowNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        qualityRowNameTextBlock.SetValue(Grid.ColumnProperty, 0);
                        qualityRowNameTextBlock.SetValue(Grid.RowProperty,
                            mainGridRowIndex); //добавить в главную гриду (фиксированную)
                        mainGrid.Children.Add(qualityRowNameTextBlock);
                        _goinSignatureTextBlocks.Add(qualityRowNameTextBlock);

                        mainGridRowIndex++;
                        for (int i = 0; i < GoInCount; i++)
                        {
                            SelectableBox stateSelectableBox = new SelectableBox(); //добавить в гриду скроллвьюера
                            stateSelectableBox.SetValue(Grid.RowProperty, scrollViewerGridRowIndex);
                            stateSelectableBox.SetValue(Grid.ColumnProperty, i);
                            stateSelectableBox.DataContext = gooseRow.SelectableValueViewModels[i];
                            scrollViewerGrid.Children.Add(stateSelectableBox);
                        }
                        scrollViewerGridRowIndex++;
                    }
                }

                foreach (var gooseRow in gooseControlBlockViewModel.GooseRowViewModels)
                {
                    if ((gooseRow.GooseRowType == "Validity"))
                    {
                        mainGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });
                        scrollViewerGrid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(CellSize) });
                        TextBlock validityRowNameTextBlock = new TextBlock();
                        validityRowNameTextBlock.Text = gooseRow.RowName;
                        validityRowNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                        validityRowNameTextBlock.SetValue(Grid.ColumnProperty, 0);
                        validityRowNameTextBlock.SetValue(Grid.RowProperty,
                            mainGridRowIndex); //добавить в главную гриду (фиксированную)
                        mainGrid.Children.Add(validityRowNameTextBlock);
                        _goinSignatureTextBlocks.Add(validityRowNameTextBlock);

                        mainGridRowIndex++;



                        for (int i = 0; i < GoInCount; i++)
                        {
                            SelectableBox stateSelectableBox = new SelectableBox(); //добавить в гриду скроллвьюера
                            stateSelectableBox.SetValue(Grid.RowProperty, scrollViewerGridRowIndex);
                            stateSelectableBox.SetValue(Grid.ColumnProperty, i);
                            stateSelectableBox.DataContext = gooseRow.SelectableValueViewModels[i];
                            scrollViewerGrid.Children.Add(stateSelectableBox);
                        }
                        scrollViewerGridRowIndex++;
                    }
                }

            }
            if (scrollViewerGridRowIndex > 0)
            {
                scrollViewer.Content = scrollViewerGrid;
                scrollViewer.SetValue(Grid.RowProperty, 1);
                scrollViewer.SetValue(Grid.RowSpanProperty, scrollViewerGridRowIndex);
                scrollViewer.SetValue(Grid.ColumnSpanProperty, GoInCount + 1);
                scrollViewer.SetValue(Grid.ColumnProperty, 1);
                scrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
                mainGrid.Children.Add(scrollViewer);
            }
        }

        #region Implementation of IDisposable


        #endregion

        private void MainGrid_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                scrollViewer.LineDown();
            }
            else
            {
                scrollViewer.LineUp();
            }
        }

        #region Implementation of IDisposable

        public void Dispose()
        {
            //Task.Run(() =>
            //{
            //    GooseGrid_Unloaded(null, null);
            //    this.mainGrid.Children.Clear();
            //});
            GooseGrid_Unloaded(null, null);
            this.mainGrid.Children.Clear();
        }

        #endregion
    }
}