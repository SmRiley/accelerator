### 声明
此项目为练手但是最后弃用的一套项目,客户端大概有80%的完成度,并不能直接部署.

服务端建议使用whmcs部署,用户信息的获取可以使用Cookie或者自己手写MySQL查询插件,最初预设的打算使用Whmcs+UnlimitedSocks的方式

Server_API可以直接放入whmcs的根目录,当时SQL文件需要覆盖UnlimitedSocks的SQL.

软件更新的话可以参考[软件更新实例](https://www.seeull.com/archives/18.html "软件更新实例")

由于项目是我刚开始学习C#的实践,所以代码写法略显幼稚,包括某些注释也不是很完整,但是界面上花了不少心思,界面是仿的国内的XX加速器.


### 截图:

![登录](https://raw.githubusercontent.com/SmRiley/Imgs/master/accelerator/1584251632.png)

![用户信息](https://raw.githubusercontent.com/SmRiley/Imgs/master/accelerator/1584251647.png)

![服务器列表](https://raw.githubusercontent.com/SmRiley/Imgs/master/accelerator/1584251665.png)

![服务器列表](https://raw.githubusercontent.com/SmRiley/Imgs/master/accelerator/1584251679.png)


## 参考
1. [原文](www.seeull.com/archives/78.html)
2. [UnlimitedSocks](https://github.com/SmRiley/accelerator)
3. [Windows下全局流量转发的应用与实现](https://www.seeull.com/archives/314.html "Windows下全局流量转发的应用与实现")
4. [Node-Tap](https://github.com/Srar/node-tap "Node-Tap")