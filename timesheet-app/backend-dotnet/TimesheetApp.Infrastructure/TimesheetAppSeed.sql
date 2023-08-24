------ Use this version with Migration DateTimeFields or newer --------


delete from Registration;
delete from Timesheet;
delete from Employee;

--dbcc checkident(Employee,reseed,0);
dbcc checkident(Registration,reseed,0);
dbcc checkident(Timesheet,reseed,0);

INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('qi0fpy805gmafi1sv2b5', 'Emma', 'Thompson', 'emmathompson@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('z4b0yo6szguqvwsvis86', 'Mia', 'Johnson', 'miajohnson@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('h4sr4n9awnwv0awkr8wy', 'Ethan', 'Baker', 'ethanbaker@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('ipche34pbovuru8qe1u2', 'Lucas', 'Martin', 'lucasmartin@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('eaw4xhdhx2k0mt01w4z3', 'Gabriel', 'Brown', 'gabrielbrown@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('zb8g0u4z7eitzha0nzzc', 'Olivia', 'Davis', 'oliviadavis@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('86dafu9ybj8qhwcz1g6t', 'Alexander', 'Rodriguez', 'alexanderrodriguez@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('7b3367w9iy0q9gfxnggf', 'Isabella', 'Ramirez', 'isabellaramirez@gmail.com', 'Employee');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('1vr60tuzdxmipdqc41u4', 'Sofia', 'Lee', 'sofialee@gmail.com', 'Admin');
INSERT INTO Employee (Id, FirstName, LastName, Email, Role) VALUES
	('7rkn2523k5g0tsjwglg1', 'William', 'Chen', 'williamchen@gmail.com', 'HR');



INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, 'qi0fpy805gmafi1sv2b5');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, 'qi0fpy805gmafi1sv2b5');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, 'qi0fpy805gmafi1sv2b5');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, 'z4b0yo6szguqvwsvis86');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, 'z4b0yo6szguqvwsvis86');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, 'z4b0yo6szguqvwsvis86');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, 'h4sr4n9awnwv0awkr8wy');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, 'h4sr4n9awnwv0awkr8wy');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, 'h4sr4n9awnwv0awkr8wy');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, 'ipche34pbovuru8qe1u2');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, 'ipche34pbovuru8qe1u2');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, 'ipche34pbovuru8qe1u2');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, 'eaw4xhdhx2k0mt01w4z3');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, 'eaw4xhdhx2k0mt01w4z3');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, 'eaw4xhdhx2k0mt01w4z3');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, 'zb8g0u4z7eitzha0nzzc');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, 'zb8g0u4z7eitzha0nzzc');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, 'zb8g0u4z7eitzha0nzzc');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, '86dafu9ybj8qhwcz1g6t');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, '86dafu9ybj8qhwcz1g6t');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, '86dafu9ybj8qhwcz1g6t');

INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 2, '7b3367w9iy0q9gfxnggf');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 3, '7b3367w9iy0q9gfxnggf');
INSERT INTO Timesheet (Year, Month, EmployeeId) VALUES (2023, 4, '7b3367w9iy0q9gfxnggf');



INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-01 9:00', '2023-02-01 17:30', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-02 9:00', '2023-02-02 17:30', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-03 9:00', '2023-02-03 17:30', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-06', '2023-02-06', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-07', '2023-02-07', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-08', '2023-02-08', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-09', '2023-02-09', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-10', '2023-02-10', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-13 9:00', '2023-02-13 17:30', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-14 8:30', '2023-02-14 16:30', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-15 8:45', '2023-02-15 17:30', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-16 8:15', '2023-02-16 17:00', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-17 8:30', '2023-02-17 17:00', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-02-20', '2023-02-20', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-02-21', '2023-02-21', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-02-22', '2023-02-22', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-02-23', '2023-02-23', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-02-24', '2023-02-24', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-27 8:30', '2023-02-27 17:00', 1);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-28 8:45', '2023-02-28 17:15', 1);

INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-01 9:00', '2023-03-01 17:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-02 9:00', '2023-03-02 17:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-03 9:00', '2023-03-03 17:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-06', '2023-03-06', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-07', '2023-03-07', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-08', '2023-03-08', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-09', '2023-03-09', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-10', '2023-03-10', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-13 9:00', '2023-03-13 17:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-14 8:30', '2023-03-14 16:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-15 8:45', '2023-03-15 17:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-16 8:15', '2023-03-16 17:00', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-17 8:30', '2023-03-17 17:00', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-20', '2023-03-20', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-21', '2023-03-21', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-22', '2023-03-22', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-23', '2023-03-23', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-24', '2023-03-24', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-27 8:30', '2023-03-27 17:00', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-28 8:45', '2023-03-28 17:15', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-29 8:30', '2023-03-29 17:30', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-30 8:45', '2023-03-30 17:15', 2);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-31 8:30', '2023-03-31 16:30', 2);



INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-01 9:00', '2023-02-01 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-02 9:00', '2023-02-02 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-03 9:00', '2023-02-03 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-06', '2023-02-06', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-07', '2023-02-07', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-08', '2023-02-08', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-09', '2023-02-09', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-02-10', '2023-02-10', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-13 9:00', '2023-02-13 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-14 8:30', '2023-02-14 16:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-15 8:45', '2023-02-15 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-16 8:15', '2023-02-16 17:00', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-17 8:30', '2023-02-17 17:00', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-20 9:00', '2023-02-20 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-21 9:00', '2023-02-21 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-22 9:00', '2023-02-22 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-23 9:00', '2023-02-23 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-02-24 9:00', '2023-02-24 17:30', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Vacationday', '2023-02-27', '2023-02-27', 4);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Vacationday', '2023-02-28', '2023-02-28', 4);

INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Vacationday', '2023-03-01', '2023-03-01', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Vacationday', '2023-03-02', '2023-03-02', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Vacationday', '2023-03-03', '2023-03-03', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-06', '2023-03-06', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-07', '2023-03-07', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-08', '2023-03-08', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-09', '2023-03-09', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Sickday', '2023-03-10', '2023-03-10', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-13 9:00', '2023-03-13 17:30', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-14 8:30', '2023-03-14 16:30', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-15 8:45', '2023-03-15 17:30', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-16 8:15', '2023-03-16 17:00', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-17 8:30', '2023-03-17 17:00', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-20', '2023-03-20', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-21', '2023-03-21', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-22', '2023-03-22', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-23', '2023-03-23', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('VacationDay', '2023-03-24', '2023-03-24', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-27 8:30', '2023-03-27 17:00', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-28 8:45', '2023-03-28 17:15', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-29 8:30', '2023-03-29 17:30', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-30 8:45', '2023-03-30 17:15', 5);
INSERT INTO Registration (RegistrationType, Start, [end], TimesheetId) VALUES
	('Workday', '2023-03-31 8:30', '2023-03-31 16:30', 5);