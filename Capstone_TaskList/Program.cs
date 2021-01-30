using System;
using System.Collections.Generic;
using System.Linq;

namespace Capstone_TaskList
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Assignment> assignments = new List<Assignment>();
            List<string> menu = new List<string>();
            menu.Add("List Tasks");
            menu.Add("Edit Task");
            menu.Add("List Tasks due before a certain date");
            menu.Add("List Task by Team Member");
            menu.Add("Add Task");
            menu.Add("Add Team Member");
            menu.Add("Delete Task");
            menu.Add("Mark Task Complete");
            menu.Add("Quit");
            List<string> teamMembers = new List<string>();
            teamMembers.Add("Add Team Member");
            teamMembers.Add("Michael Scott");
            teamMembers.Add("Dwight Schrute");
            teamMembers.Add("Pam Beesley");
            teamMembers.Add("Jim Halpert");
            teamMembers.Add("Creed Bratton");
            //teamMembers.Add("Angela Martin");
            //teamMembers.Add("Toby Flenderson");
            //teamMembers.Add("Kelly Kapoor");
            //teamMembers.Add("Kevin Malone");
            //teamMembers.Add("Stanley Hudson");
            assignments.Add(new Assignment("Michael Scott", "Plan Diversity Day", new DateTime(2019, 11, 11), false));
            assignments.Add(new Assignment("Dwight Schrute", "Create Emergency Preparedness training exercise", new DateTime(2001, 01, 4), false));
            //assignments.Add(new Assignment("Stanley Hudson", "Finish Crossword", new DateTime(2001, 01, 4), false));
            //assignments.Add(new Assignment("Pam Beesley", "Clean Keds", new DateTime(2015, 5, 28), false));
            //assignments.Add(new Assignment("Angela Martin", "Call to order the party planning commitee", new DateTime(2001, 09, 16), false));

            List<string> assignmentFields = new List<string>();
            assignmentFields.Add("Team member assigned to task:");
            assignmentFields.Add("Task Description:");
            assignmentFields.Add("Task Due Date:");


            Console.WriteLine("Welcome to the Task Manager \n");
            while (true)
            {
                WriteList(menu);

                Console.Write("\nWhat would you like to do? ");
                
                int menuSelection = NumberListChoiceValidation(menu);
                Console.WriteLine();
                if (menu[menuSelection - 1] == "List Tasks")
                {
                    ListTasks(assignments);
                }
                else if (menu[menuSelection - 1] == "Add Task")
                {
                    AddTask(assignments, teamMembers);
                }
                else if (menu[menuSelection - 1] == "Delete Task")
                {
                    ListTasks(assignments);
                    Console.WriteLine("\nWhich of the above tasks would you like to delete?");

                    int userchoice = AssignmentNumberListChoiceValidation(assignments);
                    Console.WriteLine($"Are you sure you want to remove task {userchoice}: {assignments[userchoice-1].AssignmentDescription}?");
                    bool yesNo = YesNoValidation();
                    if (yesNo == true)
                    {
                        assignments.RemoveAt(userchoice - 1);
                        Console.WriteLine("\nTask Removed\n");
                    } 
                    else if (yesNo == false)
                    { 
                        Console.WriteLine("Return to Main Menu");
                    }
                }
                else if (menu[menuSelection - 1] == "Mark Task Complete")
                {
                    Console.WriteLine("Which task would you like to mark as complete? \n");
                    ListTasks(assignments);
                    int userchoice = AssignmentNumberListChoiceValidation(assignments);
                    Console.WriteLine($"Are you sure you want to mark task {userchoice}: {assignments[userchoice - 1].AssignmentDescription} complete?");
                    bool yesNo = YesNoValidation();
                    if (yesNo == true)
                    {
                        assignments[userchoice - 1].Complete = true;
                        Console.WriteLine("\n your task was marked complete");
                    }
                    else if (yesNo == false)
                    {
                        Console.WriteLine("Return to Main Menu");
                    }
                    
                }
                else if (menu[menuSelection - 1] == "Quit")
                {
                    Console.WriteLine("Goodbye");
                    break;
                }
                else if (menu[menuSelection - 1] == "Add Team Member")
                {
                    AddTeamMember(teamMembers);
                }
                else if (menu[menuSelection - 1] == "Edit Task")
                {
                    ListTasks(assignments);

                    
                        Console.WriteLine("Which Task would you like to edit?");


                        int taskToEdit = AssignmentNumberListChoiceValidation(assignments);
                    while (true)
                    {
                        Console.WriteLine("Which field would you like to edit?");
                        WriteList(assignmentFields);
                        int fieldToEdit = NumberListChoiceValidation(assignmentFields);

                        if (fieldToEdit == 3)
                        {
                            Console.WriteLine($"Please enter a new {assignmentFields[fieldToEdit - 1]}");
                            DateTime taskDueDate = ValidateDate();
                            assignments[taskToEdit - 1].DueDate = taskDueDate;
                            Console.WriteLine($"New {assignmentFields[fieldToEdit-1]} {assignments[taskToEdit - 1].DueDate}\n");

                        }
                        else if (fieldToEdit == 1)
                        {

                            while (true)
                            {
                                Console.WriteLine("Who should the task be assigned to?");

                                WriteList(teamMembers);
                                int teamMemberNumber = NumberListChoiceValidation(teamMembers);

                                if (teamMembers[teamMemberNumber - 1] == "Add Team Member")
                                {
                                    AddTeamMember(teamMembers);

                                    continue;
                                }
                                string assignedTeamMember = teamMembers[teamMemberNumber - 1];
                                assignments[taskToEdit - 1].TeamMember = assignedTeamMember;
                                Console.WriteLine($"New {assignmentFields[fieldToEdit-1]} {assignments[taskToEdit - 1].TeamMember}\n");
                                break;
                            }
                        }
                        else if (fieldToEdit == 2)
                        {
                            Console.WriteLine($"Please enter a new {assignmentFields[fieldToEdit - 1]}");
                            assignments[taskToEdit - 1].AssignmentDescription = Console.ReadLine();
                            Console.WriteLine($"New {assignmentFields[fieldToEdit-1]} {assignments[taskToEdit - 1].AssignmentDescription}\n");
                        }
                        Console.WriteLine("Would you like to edit another field? (y/n)");
                        bool moreEdits = YesNoValidation();
                        if (moreEdits == true)
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }
                    }
                   
                }
                else if (menu[menuSelection - 1] == "List Tasks due before a certain date")
                {
                    Console.WriteLine("List tasks due before: (enter date XX/XX/XXXX)");
                    DateTime dueBeforeDate = ValidateDate();
                    if (assignments.Count == 0)
                    {
                        Console.WriteLine("There are no working tasks, add task on the main menu");
                    }
                    int i = 1;
                    foreach (Assignment task in assignments)
                    {
                        if (task.Complete == true && dueBeforeDate > task.DueDate)
                        {
                            string CompletionStatus = "Complete";
                            Console.WriteLine($"Task {i}: {task.AssignmentDescription} \nDue Date: {task.DueDate.ToLongDateString()} \nAssigned to: {task.TeamMember} \nStatus: {CompletionStatus}\n");
                            i++;
                        }
                        else if (task.Complete == false && dueBeforeDate > task.DueDate)
                        {
                            string CompletionStatus = "In Progess";
                            Console.WriteLine($"Task {i}: {task.AssignmentDescription} \nDue Date: {task.DueDate.ToLongDateString()} \nAssigned to: {task.TeamMember} \nStatus: {CompletionStatus}\n");
                            i++;
                        }

                    }
                }
                else if (menu[menuSelection - 1] == "List Task by Team Member")
                {
                    WriteList(teamMembers);

                    Console.WriteLine("You would like to see all tasks assigned to which of the above team members?");
                    int selectedTeamMember = NumberListChoiceValidation(teamMembers);
                    if (teamMembers[selectedTeamMember - 1] == "Add Team Member")
                    {
                        AddTeamMember(teamMembers);

                        continue;
                    }
                    if (assignments.Count == 0)
                    {
                        Console.WriteLine("There are no working tasks, add task on the main menu");
                    }
                    int i = 1;
                    foreach (Assignment task in assignments)
                    {
                        if (task.Complete == true && task.TeamMember == teamMembers[selectedTeamMember-1])
                        {
                            string CompletionStatus = "Complete";
                            Console.WriteLine($"Task {i}: {task.AssignmentDescription} \nDue Date: {task.DueDate.ToLongDateString()} \nAssigned to: {task.TeamMember} \nStatus: {CompletionStatus}\n");
                            i++;
                        }
                        else if (task.Complete == false && task.TeamMember == teamMembers[selectedTeamMember-1])
                        {
                            string CompletionStatus = "In Progess";
                            Console.WriteLine($"Task {i}: {task.AssignmentDescription} \nDue Date: {task.DueDate.ToLongDateString()} \nAssigned to: {task.TeamMember} \nStatus: {CompletionStatus}\n");
                            i++;
                        }

                    }

                }

                Console.WriteLine("Would you like to continue? Press Y to return to the main menu. Press N to Quit.\n");
                bool returnToMainMenu = YesNoValidation();
                if (returnToMainMenu == true)
                {
                    Console.WriteLine();
                    continue;
                } 
                else if (returnToMainMenu == false)
                {
                    break;
                }

            }




        }

        public static void WriteList(List<string> menu)
        {
            int i = 1;
            foreach (string menuItem in menu)
            {
                Console.WriteLine($"{i}) {menuItem}");
                i++;
            }
        }

        public static int NumberListChoiceValidation(List<string> list)
        {

            while (true)
            {
                string userInput = Console.ReadLine();
                int listNumber;
                bool inputIsNumber = int.TryParse(userInput, out listNumber);
                bool numberOutOfRange = listNumber > list.Count() || listNumber < 1;

                try
                {
                    
                    if (numberOutOfRange)
                    {
                        throw new Exception("Index Out of Range");
                    }
                    if (inputIsNumber)
                    {
                        return listNumber;
                    }
                    else
                    {
                        throw new Exception("Invalid Input Type");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Let's Try Again. What would you like to do? \n");
                    WriteList(list);

                }

            }
        }

        public static void ListTasks(List<Assignment> assignments)
        {
            if (assignments.Count==0)
            {
                Console.WriteLine("There are no working tasks, add task on the main menu");
            }
            int i = 1;
            foreach (Assignment task in assignments)
            {
                if (task.Complete == true)
                {
                    string CompletionStatus = "Complete";
                    Console.WriteLine($"Task {i}: {task.AssignmentDescription} \nDue Date: {task.DueDate.ToLongDateString()} \nAssigned to: {task.TeamMember} \nStatus: {CompletionStatus}\n");
                    i++;
                } 
                else
                {
                    string CompletionStatus = "In Progess";
                    Console.WriteLine($"Task {i}: {task.AssignmentDescription} \nDue Date: {task.DueDate.ToLongDateString()} \nAssigned to: {task.TeamMember} \nStatus: {CompletionStatus}\n");
                    i++;
                }
                
            }
        }

        public static void AddTask(List<Assignment> assignments, List<string> teamMembers)
        {
            while (true)
            {
                Console.WriteLine("Who should the task be assigned to?");

                WriteList(teamMembers);
                int teamMemberNumber = NumberListChoiceValidation(teamMembers);

                if (teamMembers[teamMemberNumber - 1] == "Add Team Member")
                {
                    AddTeamMember(teamMembers);

                    continue;
                }
                string assignedTeamMember = teamMembers[teamMemberNumber - 1];

                Console.WriteLine($"Please Enter a Task Description for {assignedTeamMember}");
                string taskDescription = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine($"What is the due date for {taskDescription}");
                DateTime taskDueDate = ValidateDate();
          

                assignments.Add(new Assignment(assignedTeamMember, taskDescription, taskDueDate, false));

                Console.WriteLine("\nYou added a task!");
                Console.WriteLine();
                Console.WriteLine($"Task Description: {assignments[assignments.Count() - 1].AssignmentDescription}");
                Console.WriteLine($"Assigned to: {assignments[assignments.Count() - 1].TeamMember}");
                Console.WriteLine($"Due Date: {assignments[assignments.Count() - 1].DueDate.ToLongDateString()}\n\n");

                break;
            }

        }
        public static DateTime ValidateDate()
        {
            bool validating = true;

            while (validating == true)
            {
                string date = Console.ReadLine();
                DateTime outputDate;
                bool validDate = DateTime.TryParse(date, out outputDate);
                if (validDate == true)
                {
                   
                    return outputDate;
                }
                else
                {
                    Console.WriteLine("Invalid Date. Please Enter a valid Due date (xx/xx/xxxx)");
                    continue;
                }

            }
            return new DateTime(0000, 00, 00, 0, 0, 0);
        }
        public static void AddTeamMember(List<string> name)
        {

            while (true)
            {
                Console.WriteLine("Enter a name to be added to your team");
                string userEntry = Console.ReadLine();
                if (string.IsNullOrEmpty(userEntry))
                {
                    Console.WriteLine("Invalid Name. Please Enter a name to be added to your team.");
                    continue;
                }
                else
                {
                    name.Add(userEntry);
                    break;
                }
            }

        }

        public static int AssignmentNumberListChoiceValidation(List<Assignment> list)
        {
            while (true)
            {
                string userInput = Console.ReadLine();
                int listNumber;
                bool inputIsNumber = int.TryParse(userInput, out listNumber);
                bool numberOutOfRange = listNumber > list.Count() || listNumber < 1;

                try
                {
                    if (numberOutOfRange)
                    {
                        throw new Exception("Index Out of Range");
                    }
                    if (inputIsNumber)
                    {
                        return listNumber;
                    }
                    else
                    {
                        throw new Exception("Invalid Input Type");

                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Let's Try Again. What would you like to do? \n");
                    ListTasks(list);

                }

            }
        }

        public static bool YesNoValidation()
        {
           while(true)
            {
                string userInput=Console.ReadLine().ToLower();
                if (userInput == "y")
                {
                    return true;
                }
                else if (userInput == "n")
                {
                    return false;
                }
                else
                {
                    Console.WriteLine("Invalid Input! Y/N");
                    continue;
                }

            }
        }
    }
}
