using System;

public interface IPasswordInput
{
    event Action OnCorrect;
    event Action OnIncorrect;
    void CheckInput(string input);
}