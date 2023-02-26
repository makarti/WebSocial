CREATE DATABASE `WebSocial` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

CREATE TABLE `Account` (
  `Id` varchar(36) NOT NULL,
  `Login` varchar(45) NOT NULL COMMENT '	',
  `Password` varchar(100) NOT NULL,
  `FirstName` varchar(45) NOT NULL,
  `LastName` varchar(45) NOT NULL,
  `Age` tinyint NOT NULL,
  `Gender` tinyint(1) NOT NULL,
  `Interests` varchar(255) NOT NULL,
  `City` varchar(100) NOT NULL,
  `CreateDate` datetime NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  UNIQUE KEY `Login_UNIQUE` (`Login`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

CREATE TABLE `Friendship` (
  `RequesterId` varchar(36) NOT NULL,
  `AddresserId` varchar(36) NOT NULL,
  `Created` datetime NOT NULL,
  `Status` tinyint(1) NOT NULL DEFAULT (0),
  PRIMARY KEY (`RequesterId`,`AddresserId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

