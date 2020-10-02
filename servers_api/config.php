<?php
//中国时区
date_default_timezone_set('PRC');
//抑制报错,线上环境开启
//error_reporting(0);

//whmcs Mysql
$db_host = 'localhost';
$db_port = '3306';
$db_user = 'detest';
$db_pass = 'detest';
$db_name = 'detest';
$mysql_charset = 'utf8';
//sock MySQL
$sock_host = 'localhost';
$sock_port = '3306';
$sock_user = 'socks';
$sock_pass= 'socks';
$sock_name = 'socks';
$mysql_charset = 'utf8';
//PDO
$dms = sprintf("mysql:host=%s;dbname=%s;port=%s",$db_host,$db_name,$db_port);
try{ 
    $PDO = new PDO($dms,$db_name,$db_pass);
    $PDO->setAttribute(PDO::ATTR_ERRMODE,PDO::ERRMODE_SILENT);
    $PDO->query('set names '.$mysql_charset);
}catch(PDOException $e){
    echo "Cann't connect WHMCS Mysql server";
    exit();
    $log_text = "error:cann't connect Whmcs Mysql server".date("Y.m.d - H:i:s")."\n";
    fwrite($log_file,$log_text);
}
//Sock PDO
$dms = sprintf("mysql:host=%s;dbname=%s;port=%s",$sock_host,$sock_name,$sock_port);
try{ 
    $Sock_PDO = new PDO($dms,$sock_name,$sock_pass);
    $Sock_PDO->setAttribute(PDO::ATTR_ERRMODE,PDO::ERRMODE_SILENT);
    $Sock_PDO->query('set names '.$mysql_charset);
}catch(PDOException $e){
    echo "Cann't connect WHMCS Mysql server";
    exit();
    $log_text = "error:cann't connect Socks Mysql server".date("Y.m.d - H:i:s")."\n";
    fwrite($log_file,$log_text);
}