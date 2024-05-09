using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCommandBase
{
    private string _commandName;
    private string _commandDescription;
    private string _commandFormat;

    public DebugCommandBase(string commandName, string commandDescription, string commandFormat) {
        _commandName = commandName;
        _commandDescription = commandDescription;
        _commandFormat = commandFormat;
    }

    public string GetCommandName() {
        return _commandName;
    }

    public string GetCommandDescription() {
        return _commandDescription;
    }

    public string GetCommandFormat() {
        return _commandFormat;
    }
}

public class DebugCommand : DebugCommandBase {
    private Action command;

    public DebugCommand(string commandName, string commandDescription, string commandFormat, Action command) : base (commandName, commandDescription, commandFormat) {
        this.command = command;
    }

    public void Invoke() {
        command.Invoke();
    }
} 
