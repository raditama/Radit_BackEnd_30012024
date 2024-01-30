CREATE TABLE `customer` (
	`accountId` INT(10) NOT NULL AUTO_INCREMENT,
	`name` VARCHAR(255) NOT NULL COLLATE 'utf8mb4_general_ci',
	`points` INT(10) NULL DEFAULT NULL,
	PRIMARY KEY (`accountId`) USING BTREE
)

CREATE TABLE `transaction` (
	`transactionId` INT(10) NOT NULL AUTO_INCREMENT,
	`accountId` INT(10) NULL DEFAULT NULL,
	`transactionDate` DATETIME NULL DEFAULT NULL,
	`description` VARCHAR(255) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`debitCreditStatus` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8mb4_general_ci',
	`amount` INT(10) NULL DEFAULT NULL,
	PRIMARY KEY (`transactionId`) USING BTREE,
	INDEX `accountId` (`accountId`) USING BTREE,
	CONSTRAINT `transaction_ibfk_1` FOREIGN KEY (`accountId`) REFERENCES `customer` (`accountId`) ON UPDATE NO ACTION ON DELETE NO ACTION
)
COLLATE='utf8mb4_general_ci'
ENGINE=InnoDB
AUTO_INCREMENT=29
;
