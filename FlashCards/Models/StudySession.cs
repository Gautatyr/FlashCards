﻿namespace FlashCards.Models;

internal class StudySession
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string score { get; set; }
    public int StackId { get; set;}
}