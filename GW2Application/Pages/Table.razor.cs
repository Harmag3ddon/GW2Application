using MudBlazor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Element = GW2Application.Model.Element;
using Microsoft.JSInterop;

namespace GW2Application.Pages
{
    public partial class Table
    {
        float canvasheight = 88f;
        [Inject] ISnackbar Snackbar { get; set; }

        private List<string> editEvents = new();
        private bool dense = false;
        private bool hover = true;
        private bool ronly = false;
        private bool canCancelEdit = false;
        private bool blockSwitch = false;
        private string searchString = "";
        private Element selectedItem1;
        private Element elementBeforeEdit;
        private HashSet<Element> selectedItems1 = new HashSet<Element>();
        private TableApplyButtonPosition applyButtonPosition = TableApplyButtonPosition.End;
        private TableEditButtonPosition editButtonPosition = TableEditButtonPosition.End;
        private TableEditTrigger editTrigger = TableEditTrigger.RowClick;
        private IEnumerable<Element> Elements = new List<Element>();

        protected override async Task OnInitializedAsync()
        {
            Elements = new List<Element>()
            {
                new Element()
                {
                    Number = 13,
                    Sign = "Al",
                    Name = "Aluminium",
                    Position = 12,
                    Molar = 26.981539
                },
                new Element()
                {
                    Number = 51,
                    Sign = "Sb",
                    Name = "Antimony",
                    Position = 14,
                    Molar = 121.76
                },
            };
        }

        private void ClearEventLog()
        {
            editEvents.Clear();
        }
        private void AddEditionEvent(string message)
        {
            editEvents.Add(message);
            StateHasChanged();
        }
        private void BackupItem(object element)
        {
            elementBeforeEdit = new()
            {
                Sign = ((Element)element).Sign,
                Name = ((Element)element).Name,
                Molar = ((Element)element).Molar,
                Position = ((Element)element).Position,
            };
            AddEditionEvent($"RowEditPreview event: made a backup of Element {((Element)element).Name}");
        }
        private void ItemHasBeenComitted(object element)
        {
            AddEditionEvent($"RoWCommit event:Changes to Element {((Element)element).Name} commited");
        }

        private void ResetItemToOriginalValues(object element)
        {
            ((Element)element).Sign = elementBeforeEdit.Sign;
            ((Element)element).Name = elementBeforeEdit.Name;
            ((Element)element).Molar = elementBeforeEdit.Molar;
            ((Element)element).Position = elementBeforeEdit.Position;
            AddEditionEvent($"RowEditCancel event: Editing of Element {((Element)element).Name} canceled");
        }

        private bool FilterFunc(Element element)
        {
            if (string.IsNullOrEmpty(searchString))
                return true;
            if(element.Sign.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (element.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if ($"{element.Number} {element.Position} {element.Molar}".Contains(searchString))
                return true;
            return false;
        }
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}