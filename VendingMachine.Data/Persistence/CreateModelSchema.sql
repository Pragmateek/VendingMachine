CREATE TABLE VendingMachineState
(
	Id INTEGER PRIMARY KEY AUTOINCREMENT,
	Name TEXT,
	CatalogState TEXT,
	AcceptedCoinsTypesNames TEXT,
	StoreSlotsCapacity TEXT,
	CashRegisterSlotsCapacity TEXT,
	StoreState TEXT,
    CashRegisterState TEXT,
    ControlPanelState TEXT
);

CREATE TABLE StoreState(Id INTEGER PRIMARY KEY AUTOINCREMENT, VendingMachineState_Id INTEGER);
CREATE TABLE StoreSlotState(Id INTEGER PRIMARY KEY AUTOINCREMENT, CatalogProductName TEXT, Capacity INTEGER, Count INTEGER, StoreState_Id INTEGER);
CREATE TABLE CashRegisterState(Id INTEGER PRIMARY KEY AUTOINCREMENT);
CREATE TABLE ControlPanelState(Id INTEGER PRIMARY KEY AUTOINCREMENT, InsertedCoinsTypesNames TEXT);