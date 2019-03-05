﻿using System;
namespace eNote.Models
{
    public enum MenuItemType
    {
        Home,
        NotesList,
        CreateNote,
        Accounts,
        Email,
        Phone,
        SecureFiles,
        Help,
        RateUs,
        FreeMember,
        CreateAccount,
        Settings,
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