using C_Sharp_Forum3.Context;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Windows.Markup;
using System.Windows.Controls.Primitives;

namespace C_Sharp_Forum3.Views.Pages.Users
{
    /// <summary>
    /// Interaction logic for UserViewData.xaml
    /// </summary>
    public partial class UserViewData : Page
    {
        public UserViewData()
        {
            InitializeComponent();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        // Загрузка страницы
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            dataView.ItemsSource = dbContext.db.Product.ToList();
        }

        private void buttonPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintVisual(this, "Window Print");
        }

        private void buttonCreate_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserCreateProduct());
        }
    }
}

public class WpfPrinting
{
    public const double cm = 37;
    public double Margin = 0.5 * cm;
    public double PageWidth = 21 * cm;
    public double PageHeight = 29 * cm;
    public double RowHeight = 0.7 * cm;
    public bool PageNumberVisibility = true;
    public bool DateVisibility = true;
    public double FontSize = 14;
    public double HeaderFontSize = 14;
    public bool IsBold = false;
    public void PrintDataGrid(FrameworkElement header, DataGrid grid, FrameworkElement footer, PrintDialog printDialog)
    {
        if (header == null) { header = new FrameworkElement(); header.Width = 1; header.Height = 1; }
        if (footer == null) { footer = new FrameworkElement(); footer.Width = 1; footer.Height = 1; }
        if (grid == null) return;

        System.Windows.Size pageSize = new System.Windows.Size(PageWidth, PageHeight);

        FixedDocument fixedDoc = new FixedDocument();
        fixedDoc.DocumentPaginator.PageSize = pageSize;

        double GridActualWidth = grid.ActualWidth == 0 ? grid.Width : grid.ActualWidth;

        double PageWidthWithMargin = pageSize.Width - Margin * 2;
        double PageHeightWithMargin = pageSize.Height - Margin * 2;



        // scale the header  
        double headerScale = (header?.Width ?? 0) / PageWidthWithMargin;
        double headerWidth = PageWidthWithMargin;
        double headerHeight = (header?.Height ?? 0) * headerScale;
        header.Height = headerHeight;
        header.Width = headerWidth;
        // scale the footer  
        double footerScale = (footer?.Width ?? 0) / PageWidthWithMargin;
        double footerWidth = PageWidthWithMargin;
        double footerHeight = (footer?.Height ?? 0) * footerScale;
        footer.Height = footerHeight;
        footer.Width = footerWidth;

        int pageNumber = 1;
        string Now = DateTime.Now.ToShortDateString();

        //add the header 
        FixedPage fixedPage = new FixedPage();
        fixedPage.Background = System.Windows.Media.Brushes.White;
        fixedPage.Width = pageSize.Width;
        fixedPage.Height = pageSize.Height;

        FixedPage.SetTop(header, Margin);
        FixedPage.SetLeft(header, Margin);

        fixedPage.Children.Add(header);
        // its like cursor for current page Height to start add grid rows
        double CurrentPageHeight = headerHeight + 1 * cm;
        int lastRowIndex = 0;
        bool IsFooterAdded = false;

        for (; ; )
        {
            int AvaliableRowNumber;


            var SpaceNeededForRestRows = (CurrentPageHeight + (grid.Items.Count - lastRowIndex) * RowHeight);

            //To avoid printing the footer in a separate page
            if (SpaceNeededForRestRows > (pageSize.Height - footerHeight - Margin) && (SpaceNeededForRestRows < (pageSize.Height - Margin)))
                AvaliableRowNumber = (int)((pageSize.Height - CurrentPageHeight - Margin - footerHeight) / RowHeight);
            // calc the Avaliable Row acording to CurrentPageHeight
            else AvaliableRowNumber = (int)((pageSize.Height - CurrentPageHeight - Margin) / RowHeight);

            // create new page except first page cause we created it prev
            if (pageNumber > 1)
            {
                fixedPage = new FixedPage();
                fixedPage.Background = System.Windows.Media.Brushes.White;
                fixedPage.Width = pageSize.Width;
                fixedPage.Height = pageSize.Height;
            }

            // create new data grid with  columns width and binding
            DataGrid gridToAdd;
            gridToAdd = GetDataGrid(grid, GridActualWidth, PageWidthWithMargin);


            FixedPage.SetTop(gridToAdd, CurrentPageHeight); // top margin
            FixedPage.SetLeft(gridToAdd, Margin); // left margin

            // add the avaliable rows to the cuurent grid 
            for (int i = lastRowIndex; i < grid.Items.Count && i < AvaliableRowNumber + lastRowIndex; i++)
            {
                gridToAdd.Items.Add(grid.Items[i]);
            }
            lastRowIndex += gridToAdd.Items.Count + 1;

            // add date
            TextBlock dateText = new TextBlock();
            if (DateVisibility) dateText.Visibility = Visibility.Visible;
            else dateText.Visibility = Visibility.Hidden;
            dateText.Text = Now;

            // add page number
            TextBlock PageNumberText = new TextBlock();
            if (PageNumberVisibility) PageNumberText.Visibility = Visibility.Visible;
            else PageNumberText.Visibility = Visibility.Hidden;
            PageNumberText.Text = "Page : " + pageNumber;

            FixedPage.SetTop(dateText, PageHeightWithMargin);
            FixedPage.SetLeft(dateText, Margin);

            FixedPage.SetTop(PageNumberText, PageHeightWithMargin);
            FixedPage.SetLeft(PageNumberText, PageWidthWithMargin - PageNumberText.Text.Length * 10);

            fixedPage.Children.Add(gridToAdd);
            fixedPage.Children.Add(dateText);
            fixedPage.Children.Add(PageNumberText);

            // calc Current Page Height to know the rest Height of this page
            CurrentPageHeight += gridToAdd.Items.Count * RowHeight;

            // all grid rows added
            if (lastRowIndex >= grid.Items.Count)
            {
                // if footer have space it will be added to the same page
                if (footerHeight < (PageHeightWithMargin - CurrentPageHeight))
                {
                    FixedPage.SetTop(footer, CurrentPageHeight + Margin);
                    FixedPage.SetLeft(footer, Margin);

                    fixedPage.Children.Add(footer);
                    IsFooterAdded = true;
                }
            }

            fixedPage.Measure(pageSize);
            fixedPage.Arrange(new Rect(new System.Windows.Point(), pageSize));
            fixedPage.UpdateLayout();

            PageContent pageContent = new PageContent();
            ((IAddChild)pageContent).AddChild(fixedPage);
            fixedDoc.Pages.Add(pageContent);

            pageNumber++;
            // go to start position : New page Top
            CurrentPageHeight = Margin;

            // this mean that lastRowIndex >= grid.Items.Count  and the footer dont have enough space
            if (lastRowIndex >= grid.Items.Count && !IsFooterAdded)
            {
                FixedPage ffixedPage = new FixedPage();
                ffixedPage.Background = System.Windows.Media.Brushes.White;
                ffixedPage.Width = pageSize.Width;
                ffixedPage.Height = pageSize.Height;

                FixedPage.SetTop(footer, Margin);
                FixedPage.SetLeft(footer, Margin);

                TextBlock fdateText = new TextBlock();
                if (DateVisibility) fdateText.Visibility = Visibility.Visible;
                else fdateText.Visibility = Visibility.Hidden;
                dateText.Text = Now;

                TextBlock fPageNumberText = new TextBlock();
                if (PageNumberVisibility) fPageNumberText.Visibility = Visibility.Visible;
                else fPageNumberText.Visibility = Visibility.Hidden;
                fPageNumberText.Text = "Page : " + pageNumber;

                FixedPage.SetTop(fdateText, PageHeightWithMargin);
                FixedPage.SetLeft(fdateText, Margin);

                FixedPage.SetTop(fPageNumberText, PageHeightWithMargin);
                FixedPage.SetLeft(fPageNumberText, PageWidthWithMargin - PageNumberText.ActualWidth);

                ffixedPage.Children.Add(footer);
                ffixedPage.Children.Add(fdateText);
                ffixedPage.Children.Add(fPageNumberText);

                ffixedPage.Measure(pageSize);
                ffixedPage.Arrange(new Rect(new System.Windows.Point(), pageSize));
                ffixedPage.UpdateLayout();

                PageContent fpageContent = new PageContent();
                ((IAddChild)fpageContent).AddChild(ffixedPage);
                fixedDoc.Pages.Add(fpageContent);
                IsFooterAdded = true;
            }

            if (IsFooterAdded)
            {
                break;
            }
        }
        PrintFixedDocument(fixedDoc, printDialog);
    }

    private void PrintFixedDocument(FixedDocument fixedDoc, PrintDialog printDialog)
    {
        throw new NotImplementedException();
    }

    private DataGrid GetDataGrid(DataGrid grid, double GridActualWidth, double PageWidthWithMargin)
    {
        DataGrid printed = new DataGrid();


        // styling the grid
        Style rowStyle = new Style(typeof(DataGridRow));
        rowStyle.Setters.Add(new Setter(Control.BackgroundProperty, System.Windows.Media.Brushes.White));
        rowStyle.Setters.Add(new Setter(Control.FontSizeProperty, FontSize));
        if (IsBold) rowStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.Bold));
        rowStyle.Setters.Add(new Setter(Control.HeightProperty, RowHeight));


        Style columnStyle = new Style(typeof(DataGridColumnHeader));
        columnStyle.Setters.Add(new Setter(Control.FontSizeProperty, HeaderFontSize));
        columnStyle.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
        columnStyle.Setters.Add(new Setter(Control.BorderThicknessProperty, new Thickness(0, 0.5, 0, 1.5)));
        columnStyle.Setters.Add(new Setter(Control.BackgroundProperty, System.Windows.Media.Brushes.White));
        columnStyle.Setters.Add(new Setter(Control.BorderBrushProperty, System.Windows.Media.Brushes.Black));
        columnStyle.Setters.Add(new Setter(Control.FontWeightProperty, FontWeights.SemiBold));


        printed.RowStyle = rowStyle;

        printed.VerticalGridLinesBrush = System.Windows.Media.Brushes.Black;
        printed.HorizontalGridLinesBrush = System.Windows.Media.Brushes.Black;
        printed.FontFamily = new System.Windows.Media.FontFamily("Arial");

        printed.RowBackground = System.Windows.Media.Brushes.White;
        printed.Background = System.Windows.Media.Brushes.White;
        printed.Foreground = System.Windows.Media.Brushes.Black;
        // get the columns of grid 
        foreach (var column in grid.Columns)
        {
            if (column.Visibility != Visibility.Visible) continue;
            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.HeaderStyle = columnStyle;
            textColumn.Header = column.Header;
            textColumn.Width = column.ActualWidth / GridActualWidth * PageWidthWithMargin;
            textColumn.Binding = ((DataGridTextColumn)column).Binding;
            printed.Columns.Add(textColumn);
        }
        printed.BorderBrush = System.Windows.Media.Brushes.Black;
        return printed;
    }

}