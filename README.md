# Visualize-Missing-Web-Traffic-Data-Using-AI-in-a-MAUI-Chart

This sample demonstrates how to integrate AI services and handle missing and abnormal data in the MAUI Cartesian Chart

## Sample

```xaml
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>

        <chart:SfCartesianChart Grid.Row="0" x:Name="Chart" Margin="5" PaletteBrushes="{Binding PaletteBrushes}">

            <chart:SfCartesianChart.Title>
                <StackLayout Orientation="Vertical">
                    <Label Text="E-Commerce Website Traffic Data" FontSize="18" FontAttributes="Bold" HorizontalTextAlignment="Center" />
                    <Label Text="AI-powered data cleaning and preprocessing every hour, tracking hourly website visitors" LineBreakMode="WordWrap" HorizontalTextAlignment="Center" FontSize="14"/>
                </StackLayout>
            </chart:SfCartesianChart.Title>

            <chart:SfCartesianChart.XAxes>
                <chart:DateTimeAxis ShowMajorGridLines="False" EdgeLabelsDrawingMode="Shift">
                    <chart:DateTimeAxis.LabelStyle>
                        <chart:ChartAxisLabelStyle LabelFormat="hh tt"/>
                    </chart:DateTimeAxis.LabelStyle>
                </chart:DateTimeAxis>
            </chart:SfCartesianChart.XAxes>

            <chart:SfCartesianChart.YAxes>
                <chart:NumericalAxis ShowMajorGridLines="False" Minimum="140" Interval="30" Maximum="320" EdgeLabelsDrawingMode="Center">
                </chart:NumericalAxis>
            </chart:SfCartesianChart.YAxes>

            <chart:LineSeries x:Name="CleanedDataseries" ItemsSource="{Binding CleanedData}"
                        XBindingPath="DateTime" YBindingPath="Visitors"
                        StrokeWidth="2"/>

            <chart:LineSeries x:Name="RawDataSeries" ItemsSource="{Binding RawData}" 
                        XBindingPath="DateTime" YBindingPath="Visitors" 
                        StrokeWidth="2"/>

        </chart:SfCartesianChart>

        <core:SfBusyIndicator Grid.Row="0" IsVisible="{Binding IsBusy}"
                        IsRunning="{Binding IsBusy}" AnimationType="DoubleCircle"/>

    </Grid>
```

## Requirements to run the demo

To run the demo, refer to [System Requirements for .NET MAUI](https://help.syncfusion.com/maui/system-requirements)

## Troubleshooting:
### Path too long exception

If you are facing path too long exception when building this example project, close Visual Studio and rename the repository to short and build the project.

## License

Syncfusion has no liability for any damage or consequence that may arise from using or viewing the samples. The samples are for demonstrative purposes. If you choose to use or access the samples, you agree to not hold Syncfusion liable, in any form, for any damage related to use, for accessing, or viewing the samples. By accessing, viewing, or seeing the samples, you acknowledge and agree Syncfusion's samples will not allow you seek injunctive relief in any form for any claim related to the sample. If you do not agree to this, do not view, access, utilize, or otherwise do anything with Syncfusion's samples.
