// CustomLibrary.cpp : Defines the entry point for the DLL application.
//
#include "stdafx.h"

// This forces structures to be packed on 1-byte boundaries
#pragma pack(1)

// Simple enumeration
enum EmployeeType {GRUNT, SALES_EMPLOYEE, MANAGER};

// A simple C/C++ structure
struct Employee
{
   char FirstName[20];
   char LastName[30];
   double Wage;
   short Hours;

   EmployeeType EmpType;   // What kind of employee are you?
   union                   // This field changes based on employee type
   {
      double Commission;   // Used for sales employees
      int    StockOptions; // Used for managers
      short  Zero;         // Used for grunts
   };
};

// Initializes an employee structure with default values
extern "C" _declspec(dllexport) 
void InitializeEmployee(Employee* emp, EmployeeType empType)
{
   strcpy(emp->FirstName, "Homer");
   strcpy(emp->LastName, "Simpson");
   emp->Hours = 40;
   emp->Wage = 15.5;
   emp->EmpType = empType;

   switch(empType)
   {
   case SALES_EMPLOYEE: // SalesEmployees earn commissions
      emp->Commission = 300.75;
      break;
   case MANAGER:        // Managers get stock options
      emp->StockOptions = 500;
      break;
   default:             // Grunts get squat
      emp->Zero = 0;
   }
}

BOOL APIENTRY DllMain( HANDLE hModule, 
                       DWORD  ul_reason_for_call, 
                       LPVOID lpReserved
					 )
{
    return TRUE;
}