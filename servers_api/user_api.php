<?php
    include_once('./config.php');
    $User = isset($_GET["user"]) ? $_GET["user"] : "";
    $Pass = isset($_GET["pass"]) ? $_GET["pass"] : "";
    try{
        //user and pass was existence
        if($User && $Pass){

            //预定义数组
            /*$result_Arr = array(
                "state"=>null,
                "name"=>null,
                "regdate"=>null,
                "sock_info"=>null
            );*/

            $User_sql = "select id,firstname,password from tblclients where email = ? ";
            $User_result = PDO_query($User_sql,$PDO,$User);
            $User_result = count($User_result) != 0 ? $User_result[0] : NULL;
            if($User_result != NULL){
                $Password_Hash = $User_result['password'];
                $UserID = $User_result['id'];                         
                //哈希比较
                if(password_verify($Pass,$Password_Hash)){
                    $result_Arr['state'] = "success";
                    $result_Arr["name"] = $User_result['firstname'];    
                    $Hosting_sql = "select orderid,regdate from tblhosting where userid = ? ORDER BY regdate DESC limit 1";                    
                    $Host_result = PDO_query($Hosting_sql,$PDO,$UserID);
                    //用户服务正常的情况下
                    if(!count($Host_result)<1){                        
                        $Host_regdate = strtotime($Host_result[0]['regdate']) -time();
                        $Order_ID = $Host_result[0]['orderid'];
                        $Socks_sql = "select passwd,port,method from user where sid  =  ?";                      
                        $Sock_Result = PDO_query($Socks_sql,$Sock_PDO,$Order_ID)[0];
                        $result_Arr['sock_info'] =  $Sock_Result;
                        //正常情况下服务器信息查询
                        $Servers_sql = "select ID,Region,Servers_IP,State from servers_library";                       
                    }else{
                        $Servers_sql = "select ID,Region,State from servers_library";                      
                        $Host_regdate = 0;
                    }                   
                    $Servers_result = PDO_query($Servers_sql,$Sock_PDO);
                    $result_Arr['servers_info'] = $Servers_result;
                    $result_Arr['regdate'] = $Host_regdate;
                }else{
                    //passwd was wrong
                    $result_Arr['state'] = "Pass_error";
                }
            }else{
                //user not found
               $result_Arr['state'] = "User_not_found";
            }
            echo json_encode($result_Arr);
        }
    }catch(Exception $e){
        echo "servers error";
    }

    function PDO_query($sql,$pdo,$pare = null){
        $stmt = $pdo->prepare($sql);
        if($pare != null){
            $stmt->execute([$pare]);
        }else{
            $stmt->execute();
        }       
        $result = $stmt->fetchAll(PDO::FETCH_ASSOC);
        return $result;
    }