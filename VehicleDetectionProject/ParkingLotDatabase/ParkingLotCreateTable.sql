Create table tblParkingLot (
	ParkingLotID INT NOT NULL IDENTITY PRIMARY KEY
   ,LotName VARCHAR(20) NOT NULL
   ,LotNumber VARCHAR(3) NULL
   ,MaxCapacity Int NOT NULL
   ,PermitType VARCHAR(10) NULL
);
GO
	--Student Parking Lot
	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '1', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '1A', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '2', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '3', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '3A', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '3B', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '3C', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '3D', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '4B', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '5A', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '5B', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '5C', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '7A', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '7B', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '8', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '9', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '15', 30, 'Student');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Student Lot', '18', 30, 'Student');

	--Resident Parking Lot
	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Resident Lot', '11', 30, 'Resident');

	--General Parking Lot
	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity) 
	VALUES ('General Lot', '2', 30);

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity) 
	VALUES ('General Lot', '6', 30);

	--Staff Parking Lot
	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Staff Lot', '4A', 30, 'Staff');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Staff Lot', '8', 30, 'Staff');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Staff Lot', '9A', 30, 'Staff');

	INSERT INTO tblParkingLot (LotName, LotNumber, MaxCapacity, PermitType) 
	VALUES ('Staff Lot', '15A', 30, 'Staff');

	--Solar Car Lot
	INSERT INTO tblParkingLot (LotName, MaxCapacity, PermitType) 
	VALUES ('Solar Lot', 30, 'EV');

	--Visitor Lot
	INSERT INTO tblParkingLot (LotName, MaxCapacity) 
	VALUES ('Visitor Lot', 30);

	GO
Create table tblParkingLotStatus (
	ParkingLotStatusID INT NOT NULL IDENTITY PRIMARY KEY
   ,Is_Lot_Open CHAR(1) NOT NULL
   ,Lot_Message VARCHAR(MAX) NULL
);
GO
	--Open Lot
	INSERT INTO tblParkingLotStatus (Is_Lot_Open) 
	VALUES ('Y');

	--Closed Lot
	INSERT INTO tblParkingLotStatus (Is_Lot_Open) 
	VALUES ('N');

	--Snow
	INSERT INTO tblParkingLotStatus (Is_Lot_Open, Lot_Message) 
	VALUES ('N', 'Lot is closed for snow removal');

	--Construction
	INSERT INTO tblParkingLotStatus (Is_Lot_Open, Lot_Message) 
	VALUES ('N', 'Lot is closed for construction');
GO
Create table tblLotInfoRecord (
	LotActivityTime DateTime NOT NULL PRIMARY KEY Default (GETDATE())
   ,CarParked CHAR(1) NOT NULL
   ,ParkingLotID INT REFERENCES tblParkingLot(ParkingLotID) NOT NULL
);

	--Car Parked in Student Lot 1 Sample
	--INSERT INTO tblLotInfoRecord (CarParked, ParkingLotID) 
	--VALUES ('Y' 1);
GO

Create table tblVehicles (
	VehicleID INT NOT NULL IDENTITY PRIMARY KEY
   ,Make VARCHAR(20) NOT NULL
   ,Model VARCHAR(20) NOT NULL
   ,Year VARCHAR(4) NOT NULL
);
GO
	--Gas Vehicle
	INSERT INTO tblVehicles (Make, Model, Year)
	VALUES ('Toyota', 'Camry', '2007')
	--Hybrid Vehicle
	INSERT INTO tblVehicles (Make, Model, Year)
	VALUES ('Toyota', 'Prius', '2010')
	--Electric Vehicle
	INSERT INTO tblVehicles (Make, Model, Year)
	VALUES ('Tesla', 'Model 3', '2018')
GO
Create table tblLotInfo (
	LotInfoID INT NOT NULL IDENTITY PRIMARY KEY
   ,ParkingLotID INT REFERENCES tblParkingLot(ParkingLotID) NOT NULL
   ,Num_Of_Cars_Parked INT NOT NULL Default(0) 
   ,Is_Lot_Full CHAR(1) NOT NULL
   ,ParkingLotStatusID INT REFERENCES tblParkingLotStatus(ParkingLotStatusID) NOT NULL
   ,CameraURL VARCHAR(MAX) NULL
);
GO
	--Lot Info for all lots
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (1, 0, 'N', 1)

	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (2, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (3, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (4, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (5, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (6, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (7, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (8, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (9, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (10, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (11, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (12, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (13, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (14, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (15, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (16, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (17, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (18, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (19, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (20, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (21, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (22, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (23, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (24, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (25, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (26, 0, 'N', 1)
	
	INSERT INTO tblLotInfo (ParkingLotID, Num_Of_Cars_Parked, Is_Lot_Full, ParkingLotStatusID)
	VALUES (27, 0, 'N', 1)
GO
Create table tblParkingPermit (
	ParkingPermitNum CHAR(8) NOT NULL PRIMARY KEY
   ,VehicleID INT REFERENCES tblVehicles(VehicleID) NOT NULL
   ,PermitType VARCHAR(10) NOT NULL
);
  --Student Regular Permit
  INSERT INTO tblParkingPermit (ParkingPermitNum, VehicleID, PermitType)
  VALUES('38289103', 1, 'Student')

  --Student Solar Permit
   INSERT INTO tblParkingPermit (ParkingPermitNum, VehicleID, PermitType)
  VALUES('38289104', 2, 'StudentEV')

  --Student Resident Permit
  INSERT INTO tblParkingPermit (ParkingPermitNum, VehicleID, PermitType)
  VALUES('38004404', 1, 'Resident')

  --Student Resident Solar Permit
  INSERT INTO tblParkingPermit (ParkingPermitNum, VehicleID, PermitType)
  VALUES('38284404', 2, 'ResidentEV')

  --Staff Regular Permit
  INSERT INTO tblParkingPermit (ParkingPermitNum, VehicleID, PermitType)
  VALUES('38289105', 1, 'Staff')

  --Staff Solar Permit
  INSERT INTO tblParkingPermit (ParkingPermitNum, VehicleID, PermitType)
  VALUES('38289106', 3, 'StaffEV')
  GO
Create table tblUsers (
	Email CHAR(100) Not Null PRIMARY KEY
	,UserCategory VARCHAR(12) NOT NULL
	,FirstName VARCHAR(60) NOT NULL
	,LastName VARCHAR(60) NOT NULL
	,Password VARCHAR(20) NULL --May change depending on Google Login
	,RamID CHAR(9) NULL
	,Phone VARCHAR(15) NULL
	,ParkingPermitNum Char(8) REFERENCES tblParkingPermit(ParkingPermitNum) NULL
	,FavoriteLot INT REFERENCES tblLotInfo(LotInfoID) NULL
);
GO
	--Student User
	INSERT INTO tblUsers(Email, UserCategory, FirstName, LastName, RamID, Phone, ParkingPermitNum, FavoriteLot)
	VALUES ('Wangk6@Farmingdale.edu', 'Student', 'Kevin', 'Wang', 'R01748303', '516-555-5555', '38289103', NULL)

	--Staff User
	INSERT INTO tblUsers(Email, UserCategory, FirstName, LastName, RamID, Phone, ParkingPermitNum, FavoriteLot)
	VALUES ('staffAcc@Farmingdale.edu', 'Staff', 'David', 'Gerstl', 'R01748444', '516-666-5655', '38289103', NULL)

	--Visitor User
	INSERT INTO tblUsers(Email, UserCategory, FirstName, LastName, Password, Phone, FavoriteLot)
	VALUES ('randomVisitor@gmail.com', 'Visitor', 'Jennifer', 'Green', 'ILikeKittens123!', '516-545-5655', NULL)

	--Admin User
	INSERT INTO tblUsers(Email, UserCategory, FirstName, LastName, Phone, FavoriteLot)
	VALUES ('adminAcc@Farmingdale.edu', 'Admin', 'Richard', 'Stone','516-556-5655', NULL)
	GO
--------------------------------------------------------------------------------------------
Create Trigger trVehicle_Detected 
ON tblLotInfoRecord
AFTER INSERT
AS
/*	--------------------------------------------------------------------------------------
-- Object Name: trVehicle_Detected
-- Purpose: When a vehicle enters or leaves, tblLotInfoRecord gets a new insert. 
			Update tblLotInfo 'Num_Of_Cars_Parked' based on if car left or parked on most recent insert.
			Check if lot is full and update values.
*/ --------------------------------------------------------------------------------------
BEGIN
	Declare @VehicleEntered as CHAR(1)
	Declare @ParkingLot as INT
	Declare @Num_Of_Cars_Parked as INT
	Declare @MaxCapacity as INT

	Set @VehicleEntered=(Select CarParked From Inserted) -- Parking lot entered/exit info from new record
	Set @ParkingLot=(Select ParkingLotID  From Inserted) -- Parking lot from new record
	Set @Num_Of_Cars_Parked=(Select Num_Of_Cars_Parked From tblLotInfo Where ParkingLotID = @ParkingLot) -- Current number of vehicles parked
	Set @MaxCapacity = (Select MaxCapacity From tblParkingLot Where ParkingLotID = @ParkingLot) -- Max capacity of parking lot
	IF @VehicleEntered = 'Y' --Vehicle Entered
	BEGIN
		IF @Num_Of_Cars_parked < @MaxCapacity --Do not allow space overload
		BEGIN
			UPDATE tblLotInfo
			SET Num_Of_Cars_Parked += 1 --Increment number of cars parked
			WHERE ParkingLotID = @ParkingLot
		END
	END
	ELSE --Vehicle Exited
	BEGIN
		IF @Num_Of_Cars_parked > 0 --Do not allow negatives
		UPDATE tblLotInfo
		SET Num_Of_Cars_Parked -= 1 --Increment number of cars parked
		WHERE ParkingLotID = @ParkingLot
		PRINT 'Update 2'
	END
	Set @Num_Of_Cars_Parked=(Select Num_Of_Cars_Parked From tblLotInfo Where ParkingLotID = @ParkingLot)
	--Update if parking lot is full status
	IF @MaxCapacity <= @Num_Of_Cars_Parked --If lot is equal to or more than cap, set isLotfull to true
	BEGIN
		UPDATE tblLotInfo
		SET Is_Lot_Full = 'Y'
		WHERE ParkingLotID = @ParkingLot
	END
	ELSE
	BEGIN
		UPDATE tblLotInfo
		SET Is_Lot_Full = 'N'
		WHERE ParkingLotID = @ParkingLot
	END
END
GO
--------------------------------------------------------------------------------------------

CREATE PROCEDURE spCarParked 
		@ParkingLotID INT
AS
BEGIN
/*	--------------------------------------------------------------------------------------
-- Object Name: spCarParked
-- Purpose: When a vehicle enters a spot, insert a new record in tblLotInfoRecord
-- Input Parameters: ParkingLotID (INT)
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------

--Insert Vehicle Detected
INSERT INTO tblLotInfoRecord (CarParked, ParkingLotID)
VALUES ('Y', @ParkingLotID)
END
GO
--------------------------------------------------------------------------------------------


CREATE PROCEDURE spCarLeft
		@ParkingLotID INT
AS
BEGIN
/*	--------------------------------------------------------------------------------------
-- Object Name: spCarLeft
-- Purpose: When a vehicle exits a spot, insert a new record in tblLotInfoRecord
-- Input Parameters: ParkingLotID (INT)
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------

--Insert Vehicle Detected
INSERT INTO tblLotInfoRecord (CarParked, ParkingLotID)
VALUES ('N', @ParkingLotID)
END
GO
--------------------------------------------------------------------------------------------

CREATE PROCEDURE spSetParkingLotStatus
		@Parking_Lot_ID INT,
		@Lot_Status CHAR(1),
		@Message VARCHAR(MAX) = null
AS
BEGIN
/*	--------------------------------------------------------------------------------------
-- Object Name: spSetParkingLotStatus
-- Purpose: Used to set a parking lots current status
-- Input Parameters: ParkingLotID (INT), Lot_Status (CHAR), Message (VARCHAR)
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
	DECLARE @ParkingLotStatusID INT
	SET @ParkingLotStatusID = (Select ParkingLotStatusID From tblParkingLotStatus Where Is_Lot_Open = @Lot_Status AND (Lot_Message = @Message OR @Message IS NULL))
	Print @ParkingLotStatusID

	--If status exists, set ParkingLotStatusID to tblLotInfo
	IF @ParkingLotStatusID IS NOT NULL
	BEGIN
		UPDATE tblLotInfo
		SET ParkingLotStatusID = @ParkingLotStatusID
		WHERE ParkingLotID = @Parking_Lot_ID
	END
	ELSE
	BEGIN
		INSERT INTO tblParkingLotStatus (Is_Lot_Open, Lot_Message)
		VALUES(@Lot_Status, @Message)
	END
END
GO
--------------------------------------------------------------------------------------------

Create View viewParkingLotInfo AS
/*	--------------------------------------------------------------------------------------
-- Object Name: viewParkingLotInfo
-- Purpose: Attributes used in displaying parking lot information
-- Input Parameters: 
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
Select pl.LotName, pl.LotNumber, li.Num_Of_Cars_Parked, pl.MaxCapacity, li.Is_Lot_Full, ps.Is_Lot_Open, ps.Lot_Message, pl.PermitType, li.CameraURL
From tblLotInfo as li 
Join tblParkingLot as pl 
ON li.ParkingLotID = pl.ParkingLotID
Join tblParkingLotStatus as ps
ON ps.ParkingLotStatusID = li.ParkingLotStatusID
GO
--------------------------------------------------------------------------------------------


Create View viewParkingLotMessages AS
/*	--------------------------------------------------------------------------------------
-- Object Name: viewParkingLotMessages
-- Purpose: Attributes used in displaying lot status messages
-- Input Parameters: 
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
SELECT Lot_Message
FROM dbo.tblParkingLotStatus
WHERE Lot_Message Is Not Null 
GO
--------------------------------------------------------------------------------------------

CREATE PROCEDURE spUpdateCameraURL
@ParkingLotID INT
,@CameraURL VARCHAR(MAX)
/*	--------------------------------------------------------------------------------------
-- Object Name: spUpdateCameraURL
-- Purpose: Admin can update camera URL by defining the ParkingLotID and CameraURL associated with it
-- Input Parameters: 
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
AS
UPDATE tblLotInfo
SET CameraURL = @CameraURL
WHERE ParkingLotID = @ParkingLotID
GO
--------------------------------------------------------------------------------------------

CREATE PROCEDURE spParkingLotStatus
@ParkingLotID INT
,@Status CHAR(1)
,@Message VARCHAR(50) = null
/*	--------------------------------------------------------------------------------------
-- Object Name: spParkingLotStatus
-- Purpose: Admin can update Status of a parking lot
-- Input Parameters: 
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
AS
--Check if status and message combo already exists in ParkingLotStatus
--Get ID
DECLARE @MessageStatusID INT

--If message is not empty, find possible matches by getting ID
IF @Message IS NOT NULL
BEGIN
    SET @MessageStatusID = (SELECT ParkingLotStatusID From tblParkingLotStatus WHERE Lot_Message = @Message AND Is_Lot_Open = @Status)
    --If message does exist in table, set parking lot status to id
    IF @MessageStatusID IS NOT NULL
    BEGIN
        UPDATE tblLotInfo
        SET ParkingLotStatusID = @MessageStatusID
        WHERE ParkingLotID = @ParkingLotID
    END
    ELSE --If message does not exist in table, create a new one, then update
    BEGIN
        INSERT INTO tblParkingLotStatus (Is_Lot_Open, Lot_Message)
        VALUES(@Status, @Message)

        UPDATE tblLotInfo
        SET ParkingLotStatusID = SCOPE_IDENTITY()
        WHERE ParkingLotID = @ParkingLotID

    END
END
--Set message status to 1
ELSE
BEGIN
    SET @MessageStatusID = (SELECT ParkingLotStatusID From tblParkingLotStatus WHERE Is_Lot_Open = @Status AND Lot_Message IS NULL  )
    UPDATE tblLotInfo
    SET ParkingLotStatusID = @MessageStatusID
    WHERE ParkingLotID = @ParkingLotID  
END


--------------------------------------------------------------------------------------------

CREATE PROCEDURE spParkingLotInfo
@ParkingLotID INT
,@MaxCapacity INT = NULL
,@PermitType VARCHAR(15) = NULL
/*	--------------------------------------------------------------------------------------
-- Object Name: spParkingLotInfo
-- Purpose: Admin can update parking lot information
-- Input Parameters: 
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
AS
IF (@MaxCapacity IS NOT NULL AND @PermitType IS NOT NULL) --If both are changed
BEGIN
UPDATE tblParkingLot
SET MaxCapacity = @MaxCapacity, PermitType = @PermitType
WHERE ParkingLotID = @ParkingLotID
END
ELSE IF (@MaxCapacity IS NOT NULL) --If max capacity is changed
BEGIN
UPDATE tblParkingLot
SET MaxCapacity = @MaxCapacity
WHERE ParkingLotID = @ParkingLotID
END
ELSE IF (@PermitType IS NOT NULL) --If PermitType is changed
BEGIN
UPDATE tblParkingLot
SET PermitType = @PermitType
WHERE ParkingLotID = @ParkingLotID
END
GO
--------------------------------------------------------------------------------------------


CREATE PROCEDURE spLotInfoParkedCars
@ParkingLotID INT
,@CarsParked INT
/*	--------------------------------------------------------------------------------------
-- Object Name: spLotInfoParkedCars
-- Purpose: Admin can update number of cars parked
-- Input Parameters: 
-- Created By: Kevin Wang
*/ --------------------------------------------------------------------------------------
AS
UPDATE tblLotInfo
SET Num_Of_Cars_Parked = @CarsParked
WHERE LotInfoID = @ParkingLotID

--------------------------------------------------------------------------------------------