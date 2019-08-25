# 在JJck工作用到的技术栈
=======================
###电梯<br>
* [dotnet用到的资料](https://github.com/yellowaug/jjck/blob/master/.net%E7%94%A8%E5%88%B0%E7%9A%84%E8%B5%84%E6%96%99.txt)<br>

* [运维用到的资料] (https://github.com/yellowaug/jjck/blob/master/%E5%B7%A5%E4%BD%9C%E6%97%A5%E5%B8%B8.txt)<br>

* [树莓派温度感应的源码](https://github.com/yellowaug/jjck/tree/home/Adafruit_Python_DHT)<br>
树莓派的项目已经做完实现了以下的功能：
---------------------------------
1.对温度进行实时监控，定时监控的功能还没加上
2.通过接口上传至指定的服务，不在本地存储数据
3.根据配置文件配置上传接口，工厂信息等
4.程序运行路径是~/Adafruit_Python_DHT/examples/InserToSql.py

* [账号管理系统源码](https://github.com/yellowaug/jjck/tree/home/JJCKManager)<br>
账号管理系统功能主要有以下几个
---------------------------
1.实现了登录功能，以及基本的权限管理控制
2.管理登录系统的账号
2.管理公司的虚拟机账号
3.管理公司web平台的账号
4.管理通用的账号。
<br>
* 总结
web项目的账号管理系统就到这了，不打算再动了，后面的技术会使用.net core的新技术<br>
后面会要做一下几个功能
1.文件的上传以及下载
2.尝试数据库的读写分离，看看EF的拦截器以及LOG记录等数据库原理
3.尝试.net的微服务解决方案




* [数据库自动备份源码](https://github.com/yellowaug/jjck/tree/home/JJCKsqlback)<br>
数据库自动备份的功能有以下几个：
---------------------------
1.定时备份生成数据库。
2.定时删除多余的备份文件。
3.定时删除网页的缓存文件。
4.记录操作日志。


