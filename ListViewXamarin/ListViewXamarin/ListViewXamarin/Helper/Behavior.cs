using Syncfusion.ListView.XForms;
using Syncfusion.ListView.XForms.Control.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ListViewXamarin
{
    public class Behavior : Behavior<ContentPage>
    {
        ViewModel viewModel;
        public SfListView ToDoListView { get; private set; }
        public SfListView WorkDoneListView { get; private set; }
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);
            ToDoListView = bindable.FindByName<SfListView>("ToDoListView");
            WorkDoneListView = bindable.FindByName<SfListView>("WorkDoneListView");
            viewModel = new ViewModel();
            ToDoListView.BindingContext = viewModel;
            WorkDoneListView.BindingContext = viewModel;

            ToDoListView.ItemDragging += ToDoListView_ItemDragging;
            WorkDoneListView.ItemDragging += WorkDoneListView_ItemDragging;
        }


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

        protected override void OnDetachingFrom(ContentPage bindable)
        {
            base.OnDetachingFrom(bindable);
            ToDoListView.ItemDragging -= ToDoListView_ItemDragging;
            WorkDoneListView.ItemDragging -= WorkDoneListView_ItemDragging;
            ToDoListView = null;
            WorkDoneListView = null;
        }
    }
}
