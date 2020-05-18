0. Add comments for all the files you made a change in or that you added.
Files without the following comment:
// I certify that this assignment is my own work entirely, performed independently and without any help from the sources which are not allowed.
// <full name>
WON'T BE CHECKED!

You were assigned to write an app which finds a route (not necessarily the optimal one) between two given cities.
Each city consists of:
— name,
— population,
— information whether or not it has a restaurant.

Disclaimer — we assume that the name of the city is also its unique identifier.
(All the cities in the database have unique names and each city can only be identified by its name).

Each route contains information about:
— starting city,
— destination city,
— travel price,
— travel time,
— means of transport (either car or train).
Routes are considered to be one-way (you can't travel both ways). Both price and travel time are non-negative values.
Significant portion of the code was written by your predecessor (who is an expert in algorithms but not in design patterns and who is now on vacation).
The following parts have already been written:
— DFS, BFS and Dijkstra algorithm — note that DFS and BFS are going to give the same route regardless of the type of optimization
	(price/time) and that doesn't have to be optimal in any of these metrics.
— databases of cities and communication links — they are provided externally, and we are not interested in creating them, however they can be saved as different classes
	(your predecessor created two implementations — a list of links originating from each one and a matrix of links)
	
Your task:

1. Implementation of two families of interfaces.

1a. XML
Data is going to be read and displayed in the form of XML (Value). Using Schema or other tools to read fully formatted XML is not required.

1b. Key=Value
Data is going to be read and displayed in the form of Key=Value.

The user must have the possibility to:
— choose starting city,
— choose destination city,
— choose the algorithm for route selection,
— choose optimization target (minimal time or minimal price),
— restrict cities to a certain minimal population (including the starting and ending nodes),
— restrict allowed means of transport,
— restrict cities to those that have a restaurant.

You also have to create a component presenting the route obtained in the result.

In order to do that, we need to have the possibility to input the following types:
— text,
— number,
— boolean value (true/false)

IForm with the following methods, is responsible for reading data:
— Insert — reading data records,
— GetBoolValue, GetTextValue, GetNumericValue — returning the value of the variable assigned to the field, which name is given as a parameter.

Display is going to be done through Display class, which method Print should display information about the route.

Both interfaces are part of the system (ISystem).

Disclaimers for 1a and 1b:
The addition of a new family of interfaces should not require changes in the preexisting ones.
The addition of a new option to the interface should be easy and should not require any changes in the code inside the class.
Sample files with input data and expected output are in the Samples folder.

2. Addition of the possibility to browse nodes.

Graph databases are already created, however they lack the option to browse edges going out of the nodes. This requires different behaviour depending on implementation.

2a. AdjacencyListDatabase 
In this graph, lists of outgoing paths are stored as a dictionary, which stores the list of outgoing roads for each city.
The graph doesn't have to be a simple one (multiple edges going out of v and going into u are allowed).

2b. MatrixDatabase
A list of a list of roads is stored in this graph. If a given route doesn't exist, there is a null in the corresponding place.
You can assume that the graph is a simple one without a loop (there is only one edge from v to u).

Create an interface that allows browsing outgoing edges without the need to know the implementation of the base.
Remember that when your colleague returns from vacation, they may want to create a new type of base, for example one that downloads data from the network.
Remember that you can't download all outgoing edges at once, you have to browse them one by one. 

3. Adding an interface to the existing algorithms.

Algorithms are already written and their code, beside commented part should not be changed. You should use them in your code.
However, remember that your write a different algorithm when he returns from vacation. Adding it should not require changing the other ones.
The only place that would have to be changed is the way the request is handled (more on that in section 5).
Adding new metrics in the classes is not planned (adding new type of problems may need a change in existing code).

4. Database merging and filtering
A request may require filtering of certain parts of the database (certain paths or whole cities). A request may also require merging of databases. Create a solution that will allow it.

Disclaimers:
— Note that database merging has an effect on operations on cities (a city may not exist in all databases simultaneously) and outgoing edges.
— Database filtering has an effect on outgoing edges as well.

5. The operation of the request
Operation of the request should:
— get the required data from any of the user interfaces using RequestMapper,
— check if the data is correct (non-blank 'from' and 'to' fields, non-negative minimal population, at least one means of transport is chosen),
— detect which algorithm is required,
— deliver data to the algorithm,
— allow the addition of city filters and road filters.

Disclaimers:
— The use of provided RequestMapper is required.
— The use of 'switch' and cascade or nested 'if' instructions is NOT allowed (you are only allowed to check 'if(some_value = constant)').
Moreover, the addition of a new constant allowed in the user interface (for example, a new type of algorithm) should only change the code in one place (but may require the creation of a new class).

Overall disclaimers:
— The use of keywords 'yield', 'type of', 'is', 'as', inheritance after interface IEnumerable, and lambda expressions is NOT allowed.
— The use of reflection, and any direct form of checking the type of an object is NOT allowed.
— The use of 'Func' objects is NOT allowed.
— Every file contains information whether or not (and to what degree) its modification is allowed. You can create new files and modify them, and it is recommended to do so.
— Even if file modification is allowed, code already provided within can NOT be changed.
— Remember that the task is about design patterns, not algorithms, so the graph representation was not created with great performance in mind.
