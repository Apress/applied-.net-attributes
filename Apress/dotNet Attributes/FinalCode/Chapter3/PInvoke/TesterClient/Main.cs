using System;
using System.Runtime.InteropServices;

namespace TesterClient
{
   enum EmployeeType {Grunt, SalesEmployee, Manager};

   // Define a managed Employee structure
   [StructLayout(LayoutKind.Explicit)]
   struct Employee
   {
      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=20)]
      [FieldOffset(0)]  public string FirstName;

      [MarshalAs(UnmanagedType.ByValTStr, SizeConst=30)]
      [FieldOffset(20)]  public string LastName;

      [FieldOffset(50)] public double Wage;
      [FieldOffset(58)] public short Hours;

      [FieldOffset(60)] public EmployeeType EmpType;

      // For unions, use the same field offset
      [FieldOffset(64)] public double Commission;
      [FieldOffset(64)] public int StockOptions;
      [FieldOffset(64)] public byte Zero;
   }

   class TesterMain
   {
      [DllImport(@"..\..\..\CustomLibrary\Debug\customlibrary.dll")]
      private static extern void InitializeEmployee(ref Employee emp, 
         EmployeeType empType);

      static void Main(string[] args)
      {
         Employee emp = new Employee(); 
         
         InitializeEmployee(ref emp, EmployeeType.Manager);
         Console.WriteLine(emp.FirstName);
         Console.WriteLine(emp.LastName);
         Console.WriteLine(emp.Hours);
         Console.WriteLine(emp.Wage);
         Console.WriteLine(emp.EmpType);
         Console.WriteLine(emp.StockOptions);

         Console.ReadLine();
      }
   }
}
