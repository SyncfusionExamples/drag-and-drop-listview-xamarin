# How to drag and drop an item from one to another listview in xamarin.forms?

You can drag and drop an item from one to another [SfListView](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView.html?) using the [ItemDragging](https://help.syncfusion.com/cr/cref_files/xamarin/Syncfusion.SfListView.XForms~Syncfusion.ListView.XForms.SfListView~ItemDragging_EV.html?) event in Xamarin.Forms.

This article explains you about how to drag an item from one to other listview in xamarin.forms
https://www.syncfusion.com/kb/11203/how-to-drag-and-drop-an-item-from-one-to-another-listview-in-xamarin-forms 

**XAML**

The ListView is defined with **DragStartMode**, **OnHold**, and **OnDragIndicator**.

``` xml
<ContentPage xmlns:syncfusion="clr-namespace:Syncfusion.ListView.XForms;assembly=Syncfusion.SfListView.XForms">
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition Width="*" />
            <ColumnDefinition Width="*" />
        </Grid.ColumnDefinitions>
        <syncfusion:SfListView x:Name="ToDoListView"
                               ItemSize="60"
                               IsStickyHeader="True"
                               ItemsSource="{Binding ToDoList}"
                               DragStartMode="OnHold,OnDragIndicator"
                               SelectionMode="None">
            <syncfusion:SfListView.HeaderTemplate>
                <DataTemplate>
                    <Label Text="To do Items" />
                </DataTemplate>
            </syncfusion:SfListView.HeaderTemplate>
            <syncfusion:SfListView.ItemTemplate>
                <DataTemplate>
                    <Grid>
                        <Label Text="{Binding Name}"/>
                        <syncfusion:DragIndicatorView ListView="{x:Reference ToDoListView}">
                            <Grid Padding="10, 20, 20, 20">
                                <Image Source="DragIndicator.png" />
                            </Grid>
                        </syncfusion:DragIndicatorView>
                    </Grid>
                </DataTemplate>
            </syncfusion:SfListView.ItemTemplate>
        </syncfusion:SfListView>
        <syncfusion:SfListView x:Name="WorkDoneListView"
                               ItemSize="60"
                               IsStickyHeader="True"
                               ItemsSource="{Binding NewList}"
                               DragStartMode="OnHold,OnDragIndicator"
                               SelectionMode="None">
            <syncfusion:SfListView.HeaderTemplate>
                <DataTemplate>
                    <Label Text="Completed works" />
                </DataTemplate>
            </syncfusion:SfListView.HeaderTemplate>
            <syncfusion:SfListView.ItemTemplate>
                <DataTemplate>
                    <Grid>
                        <Label Text="{Binding Name}"/>
                        <syncfusion:DragIndicatorView ListView="{x:Reference ToDoListView}">
                            <Grid Padding="10, 20, 20, 20">
                                <Image Source="DragIndicator.png" />
                            </Grid>
                        </syncfusion:DragIndicatorView>
                    </Grid>
                </DataTemplate>
            </syncfusion:SfListView.ItemTemplate>
        </syncfusion:SfListView>
    </Grid>
</ContentPage>
```
**C#**

You can check the value of the bound of the **listview** in the **ItemDragging** event using the position and you can also handle the add or remove the item from one collection to another.

Here, the dragged item is removed from the **WorkDoneListView** bound collection and added to the **ToDoListView** bound collection.

``` c#
WorkDoneListView.ItemDragging += WorkDoneListView_ItemDragging;
 
private async void WorkDoneListView_ItemDragging(object sender, ItemDraggingEventArgs e)
{
    var position = new Point();
    var xPosition = e.Position.X;
    double yPosition = e.Position.Y;
    position.X = xPosition;
    position.Y = yPosition;
    if (e.Action == DragAction.Dragging)
    {
        if (this.ToDoListView.Bounds.Contains(position))
            this.ToDoListView.BackgroundColor = Color.Red;
        else
            this.ToDoListView.BackgroundColor = Color.White;
    }
    if (e.Action == DragAction.Drop)
    {
        if (this.ToDoListView.Bounds.Contains(position))
        {
            var item = e.ItemData as ToDoItem;
            await Task.Delay(100);
            viewModel.WorkDoneList.Remove(item);
            viewModel.ToDoList.Add(item);
            item.IsDone = false;
        }
        this.ToDoListView.BackgroundColor = Color.White;
    }
}
```
**C#**

Here, the dragged item is removed from the **ToDoListView** bound collection and added to the **WorkDoneListView** bound collection.

``` c#
ToDoListView.ItemDragging += ToDoListView_ItemDragging;
 
private async void ToDoListView_ItemDragging(object sender, ItemDraggingEventArgs e)
{
    var position = new Point();
    var xPosition = e.Position.X;
    double yPosition = e.Position.Y;
    position.X = xPosition;
    position.Y = yPosition;
 
    if (e.Action == DragAction.Dragging)
    {
        if (this.WorkDoneListView.Bounds.Contains(position))
            this.WorkDoneListView.BackgroundColor = Color.Red;
        else
            this.WorkDoneListView.BackgroundColor = Color.White;
    }
    if (e.Action == DragAction.Drop)
    {
        if (this.WorkDoneListView.Bounds.Contains(position))
        {
            var item = e.ItemData as ToDoItem;
            await Task.Delay(100);
            viewModel.ToDoList.Remove(item);
            viewModel.WorkDoneList.Add(item);
            item.IsDone = true;
        }
        this.WorkDoneListView.BackgroundColor = Color.White;
    }
}
```
Note:The ListViewItem is added to the container. So, the item does not come out from it.
