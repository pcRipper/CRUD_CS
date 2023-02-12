use ZP_APSL
go

CREATE TABLE _User (
	_email    VARCHAR(40) NOT NULL PRIMARY KEY,
	_password CHAR(32) NOT NULL,
	_name	  VARCHAR(30),
	_surname  VARCHAR(30),
	_dob	  DATE,
	_sallary  FLOAT,
)
go