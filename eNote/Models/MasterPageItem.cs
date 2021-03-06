﻿using System;
namespace eNote.Models
{
    public enum MenuItemType
    {
        Home,
        NotesList,
        CreateNote,
        Accounts,
        PurchaseNotes,
        ExpensesNotes,
        AudioRecording,
        Random,
        Calculator,
        Help,
        RateUs,
        FreeMember,
        CreateAccount,
        Settings,
        Scanner,
        Version,
        ComingFeatures,
        Logout
    }
    public class MasterPageItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }
    }
}
