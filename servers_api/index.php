<?php
//载入配置文件
include_once('./config.php');
//取访问用户IP及访问用户
$user_ip = $_SERVER["REMOTE_ADDR"];
//载入日志文件
$log_file = fopen("logs.txt",'a');
//IP库查询
$sql = "select Servers_IP from servers_library";
$query_result = $PDO->query($sql);
if(is_object($query_result)){
    $ip_library = $query_result->fetchAll(PDO::FETCH_ASSOC);
}else{
    echo "致命错误:PDO查询失败"; 
    exit();
}
foreach($ip_library as $value){
    foreach($value as $v){
        if($user_ip == $v){
            echo "success";
            $sql = sprintf("INSERT INTO logs(ID,Link_Time,Link_State,Link_Server) VALUES(default,%s,'1','%s')",time(),$v);
            $result = $PDO ->exec($sql);
            if(!$result){
                //出错日志记录
                $log_text = "error:cann't instert log data to Mysql;date：".date("Y.m.d - H:i:s")."\n";
                fwrite($log_file,$log_text);
            }
            break;
        }
    }
}
?>