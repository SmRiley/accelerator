/*
 Navicat Premium Data Transfer

 Source Server         : 127.0.0.1
 Source Server Type    : MySQL
 Source Server Version : 50553
 Source Host           : localhost:3306
 Source Schema         : servers_api

 Target Server Type    : MySQL
 Target Server Version : 50553
 File Encoding         : 65001

 Date: 14/09/2019 23:50:02
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for logs
-- ----------------------------
DROP TABLE IF EXISTS `logs`;
CREATE TABLE `logs`  (
  `ID` int(255) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `Link_Time` int(25) UNSIGNED NOT NULL COMMENT '链接时间',
  `Link_State` enum('0','1') CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '链接详情',
  `Link_Server` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '被链接服务器',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 24 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;


-- ----------------------------
-- Table structure for servers_library
-- ----------------------------
DROP TABLE IF EXISTS `servers_library`;
CREATE TABLE `servers_library`  (
  `ID` int(255) UNSIGNED NOT NULL AUTO_INCREMENT COMMENT '主键ID',
  `Region` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '服务器区域',
  `Servers_IP` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL COMMENT '服务器IP',
  `State` enum('0','1') CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  `Remarks` char(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 7 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of servers_library
-- ----------------------------
INSERT INTO `servers_library` VALUES (1, '香港', '127.0.0.1', '1', '默认');
INSERT INTO `servers_library` VALUES (2, '香港', '127.0.0.1', '1', '默认');
INSERT INTO `servers_library` VALUES (3, '香港', '127.0.0.1', '1', '默认');
INSERT INTO `servers_library` VALUES (4, '香港', '127.0.0.1', '1', '默认');
INSERT INTO `servers_library` VALUES (5, '香港', '127.0.0.1', '1', '默认');

-- ----------------------------
-- Table structure for version_control
-- ----------------------------
DROP TABLE IF EXISTS `version_control`;
CREATE TABLE `version_control`  (
  `ID` int(11) NOT NULL AUTO_INCREMENT COMMENT '版本ID',
  `version` int(255) NOT NULL COMMENT '版本号',
  `dead_version` int(255) NOT NULL COMMENT '截止版本号ID',
  `download_link` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL,
  PRIMARY KEY (`ID`) USING BTREE
) ENGINE = MyISAM AUTO_INCREMENT = 2 CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of version_control
-- ----------------------------
INSERT INTO `version_control` VALUES (1, 204, 204, 'http://api.sauyoo.com/Debug.zip');

SET FOREIGN_KEY_CHECKS = 1;
