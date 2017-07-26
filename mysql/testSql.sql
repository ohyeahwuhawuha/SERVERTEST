CREATE DATABASE IF NOT EXISTS `testSql` DEFAULT CHARACTER SET utf8 COLLATE utf8_unicode_ci;
USE `testSql`;

CREATE TABLE IF NOT EXISTS `User` (
  `userId` int(11) NOT NULL AUTO_INCREMENT,
  `sceneId` int(11) NOT NULL,
  PRIMARY KEY (`userId`),
  UNIQUE KEY `INDEX_USER_TOKEN` (`userId`)
)ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_unicode_ci;
