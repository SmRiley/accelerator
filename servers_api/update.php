<?php
    include_once("./config.php");
    $sql = "select version,dead_version,download_link from version_control ORDER BY ID LIMIT 0,1";
    $query_result = $Sock_PDO->query($sql);
    if(is_object($query_result)){
        $version_info = $query_result->fetchAll(PDO::FETCH_ASSOC)[0];
    }else{
        echo "PDO查询报错";
        exit();
    }
    echo json_encode($version_info);