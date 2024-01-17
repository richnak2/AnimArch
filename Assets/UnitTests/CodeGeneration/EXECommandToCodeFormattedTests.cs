using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using OALProgramControl;
using UnityEngine;
using UnityEngine.TestTools;

public class EXECommandToCodeFormattedTests
{
    [Test]
    public void EXECommandAddingToList_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeAccessChain _assignmentTarget = new EXEASTNodeAccessChain();
        _assignmentTarget.AddElement(new EXEASTNodeLeaf("list"));
        EXEASTNodeBase _assignedValue = new EXEASTNodeLeaf("element");

        EXECommand _command = new EXECommandAddingToList(_assignmentTarget, _assignedValue);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "add element to list;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandAssignment_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeAccessChain _assignmentTarget = new EXEASTNodeAccessChain();
        _assignmentTarget.AddElement(new EXEASTNodeLeaf("variable"));
        EXEASTNodeBase _assignedValue = new EXEASTNodeLeaf("5");

        EXECommand _command = new EXECommandAssignment(_assignmentTarget, _assignedValue);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "variable = 5;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandBreak_ToCodeFormattedConversionTest() {
        // Arrange
        EXECommand _command = new EXECommandBreak();
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "break;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandCall_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeAccessChain _callSource = new EXEASTNodeAccessChain();
        _callSource.AddElement(new EXEASTNodeLeaf("firstVariable"));
        _callSource.AddElement(new EXEASTNodeLeaf("secondVariable"));
        EXEASTNodeMethodCall _callTargett = new EXEASTNodeMethodCall("function");

        EXECommand _command = new EXECommandCall(_callSource, _callTargett);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "firstVariable.secondVariable.function();\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandContinue_ToCodeFormattedConversionTest() {
        // Arrange
        EXECommand _command = new EXECommandContinue();
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "continue;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandCreateList_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeAccessChain _callSource = new EXEASTNodeAccessChain();
        _callSource.AddElement(new EXEASTNodeLeaf("firstVariable"));
        _callSource.AddElement(new EXEASTNodeLeaf("secondVariable"));
        List<EXEASTNodeBase> _elementList = new List<EXEASTNodeBase>
        {
            new EXEASTNodeLeaf("firstElement"),
            new EXEASTNodeLeaf("secondElement"),
            new EXEASTNodeLeaf("thirdElement")
        };

        EXECommand _command = new EXECommandCreateList("ListType", _callSource, _elementList);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "create list firstVariable.secondVariable of ListType { firstElement, secondElement, thirdElement };\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandMulti_ToCodeFormattedConversionTest() {
        // Arrange
        List<EXECommand> _commandList = new List<EXECommand>();

        EXECommand _command = new EXECommandMulti(_commandList);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "Command;\n"; // default EXECommand output

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandQueryCreate_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeAccessChain _callSource = new EXEASTNodeAccessChain();
        _callSource.AddElement(new EXEASTNodeLeaf("InstanceName"));

        EXECommand _command = new EXECommandQueryCreate("ClassName", _callSource);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "create object instance InstanceName of ClassName;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandQueryDelete_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeBase _deletedValue = new EXEASTNodeLeaf("variable");
        EXECommand _command = new EXECommandQueryDelete(_deletedValue);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "delete object instance variable;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandRead_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeAccessChain _callSource = new EXEASTNodeAccessChain();
        _callSource.AddElement(new EXEASTNodeLeaf("firstVariable"));
        _callSource.AddElement(new EXEASTNodeLeaf("secondVariable"));
        EXEASTNodeBase _prompt = new EXEASTNodeLeaf("prompt");

        EXECommand _command = new EXECommandRead("assignmentType", _callSource, _prompt);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "firstVariable.secondVariable = assignmentType (read( prompt ));\n"; // not sure if this is how the result should look, bud originally it was `firstVariable.secondVariable = assignmentTypeprompt))` and this seems better according to grammar, but feel free to change it

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandRemovingFromList_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeBase _element = new EXEASTNodeLeaf("element");
        EXEASTNodeBase _list = new EXEASTNodeLeaf("list");

        EXECommand _command = new EXECommandRemovingFromList(_list, _element);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "remove element from list;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandReturn_ToCodeFormattedConversionTest() {
        // Arrange
        EXEASTNodeBase _returnValue = new EXEASTNodeLeaf("value");

        EXECommand _command = new EXECommandReturn(_returnValue);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "return value;\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }

    [Test]
    public void EXECommandWrite_ToCodeFormattedConversionTest() {
        // Arrange
        List<EXEASTNodeBase> _args = new List<EXEASTNodeBase>() {
            new EXEASTNodeLeaf("arg1"),
            new EXEASTNodeLeaf("arg2")
        };

        EXECommand _command = new EXECommandWrite(_args);
        VisitorCommandToString visitor = VisitorCommandToString.BorrowAVisitor();

        // Act
        _command.Accept(visitor);
        string _actualOutput = visitor.GetCommandStringAndResetStateNow();

        // Assert
        string _expectedOutput = "write(arg1, arg2);\n";

        Assert.AreEqual(_expectedOutput, _actualOutput);
    }
}
