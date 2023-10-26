grammar OAL;

// Parser Rules
//treba asi podoplnat commenty aj do jednotlivych pravidiel //asi netreba

lines
	:	line+ EOF
	;

line
	:	exeCommandQueryCreate
	|	exeCommandQueryDelete
	|	exeCommandAssignment
	|	exeCommandCall
	|	exeCommandCreateList
	|	exeCommandAddingToList
    |	exeCommandRemovingFromList
	|	exeCommandWrite
	|	exeCommandRead
    |	returnCommand
	|	continueCommand
	|	breakCommand
	|	whileCommand
	|	ifCommand
	|	foreachCommand
	|	parCommand
	|	commentCommand
	;

commands
    : line*
    ;

parCommand
    :   'par' threadCommand+ 'end par' ';'
    ;

threadCommand
    :   'thread' commands 'end thread' ';'
    ;

ifCommand
    :   'if' condition commands (elif)* (else)? 'end if' ';'
    ;

elif
    :   'elif' condition commands
    ;

else
    :   'else' commands
    ;

condition
    :   '(' expr ')'
    ;

whileCommand
    :   'while' condition commands 'end while' ';'
    ;

foreachCommand
    :   'for each ' variableName ' in ' expr commands 'end for' ';'
    ;

continueCommand
    :   'continue' ';'
    ;

breakCommand
    :   'break' ';'
    ;

commentCommand
    :   COMMENT
    ;

exeCommandQueryCreate
    :   'create object instance ' accessChain ' of ' className ';'
    |   'create object instance of ' className ';'
    ;

exeCommandQueryDelete
    :   'delete object instance ' accessChain ';'
    ;

exeCommandAssignment
    :   ('assign ')? accessChain '=' expr ';'
    ;

exeCommandCall
    :   (accessChain '.')? methodCall ';'
    ;

exeCommandCreateList
    :   'create list ' accessChain ' of ' className (listLiteral)? ';'
    ;

listLiteral
    :   '{' params '}'
    ;

exeCommandAddingToList
    :   'add ' expr ' to ' expr ';'
    ;

exeCommandRemovingFromList
    :   'remove ' expr ' from ' expr ';'
    ;

exeCommandWrite
    :   'write' '(' (expr (',' expr)*)? ')' ';'
    ;

exeCommandRead
    :   ('assign ')? accessChain '=' (
                                        'read(' expr? ')'
                                        | 'int(read(' expr? ')' ')'
                                        | 'real(read(' expr? ')' ')'
                                        | 'bool(read(' expr? ')' ')'
                                        ) ';'
    ;

returnCommand
    :   'return' expr? ';'
    ;

expr
    :   NUM | BOOL | STRING
    |   accessChain
    |   'cardinality ' expr
    |   ('empty ' | 'not_empty ') expr
    |   bracketedExpr
    |   '-' expr 
    |   expr ('*' | '/' | '%') expr
    |   expr ('+' | '-')  expr
    |   expr ('<' | '>' | '<=' | '>=' | '==' | '!=') expr
    |   ('not ' | 'NOT ') expr
    |   expr (' and ' | ' AND ') expr
    |   expr (' or ' | ' OR ') expr
    ;

accessChain
    :   methodCall '.' accessChain
    |   NAME '.' accessChain
    |   methodCall
    |   NAME
    ;

methodCall
    :   methodName '(' params ')'
    ;

params
    :   expr (',' params)?
    ;

bracketedExpr
    :   '(' expr ')'
    ;

className
    :	NAME
    ;

variableName
    :	NAME
    ;

methodName
    :	NAME
    ;

attribute
    :	NAME
    ;

// Lexer Rules

BOOL
    :   'TRUE' | 'FALSE'
    ;

NAME
    :   [a-zA-Z_#][a-zA-Z0-9_#]*
    ;

STRING
    :   '"' (~'"' | '\\"')* '"'
    ;

NUM
    :   INT | DECIMAL
    ;

fragment INT
    :   '0'
    | [1-9][0-9]*
    ;
fragment DECIMAL
    :   '0' '.' [0-9]+
        | [1-9][0-9]*'.'[0-9]+
    ;

COMMENT
    :   '//' ~('\r' | '\n')*  // druhy priklad:  '//' .*? '/n'
    ;

WHITE_SPACE
    :   [ \t\r\n]+      // skip spaces, tabs, newlines
    -> skip
    ;