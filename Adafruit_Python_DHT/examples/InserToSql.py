import Adafruit_DHT
import  requests
import datetime
from xml.dom import minidom
import os
class DHT():
    def ReadDht(self,avgconfig):
        temparry=[]
        sensor_args = {'11': Adafruit_DHT.DHT11,
                       '22': Adafruit_DHT.DHT22,
                       '2302': Adafruit_DHT.AM2302}
        sensor=sensor_args[avgconfig[4]] #定义传感器型号
        print(sensor)
        # sensor = Adafruit_DHT.DHT11
        pin = int(avgconfig[3])#针脚PIO
        print(int(avgconfig[3]),type(avgconfig[3]))
        print(pin)
        try:
            humidity, temperature = Adafruit_DHT.read_retry(sensor, pin)
            print('Temp={0:0.1f}*C  Humidity={1:0.1f}%'.format(temperature, humidity))
            temparry.append(humidity)
            temparry.append(temperature)
            return  temparry
        except:
            print(BaseException(DHT.ReadDht))#捕获异常
            return BaseException
        # self.Humidity='{1:0.1f}'.format(humidity)
        # self.Temper='{0:0.1f}'.format(temperature)

    # def AddDataToSQL(self ,dhtTemp,dht_humidity):
    #     serverName='127.0.0.1'
    #     userID='sa'
    #     userPw='sa'
    #     Db='JJCKManagerDb'
    #     dbconnect=pymssql.connect(host=serverName,user=userID,password=userPw,database=Db)
    #     cur=dbconnect.cursor()
    #     Tsql="""insert into JJCKIot.TempFunction(Temperature,humidity,IotDevIP,FuncName,Updatadate)values(%s,%s,%s,%s,%s);"""
    #     dhttemp=dhtTemp
    #     dhthumidity=dht_humidity
    #     iotip='10.11.10.170'
    #     funcname='jjck'
    #     updatetime=datetime.datetime.now().strftime('%Y.%m.%d %H:%M:%S')
    #     values=(dhttemp,dhthumidity,iotip,funcname,updatetime)
    #     try:
    #         cur.execute(Tsql,values)
    #         dbconnect.commit()
    #         print("数据写入成功")
    #     except Exception as e:
    #         dbconnect.rollback()
    #         print(e)
    #     finally:
    #         dbconnect.close()
    def PostToServer(self,iotTemperature,iotHumidity,avgconfig):
        iotupdate=datetime.datetime.now().strftime("%Y.%m.%d %H:%M:%S")
        if(len(avgconfig)==0):
            print("请传入正确参数，以及检查配置文件")
        else:
            print(avgconfig)
            print(len(avgconfig))
            url=avgconfig[2]
            # url="http://10.12.2.15:8080/Iotwebapi/postapi"
            listdata={"Temperature":iotTemperature,
                      "humidity":iotHumidity,
                      "IotDevIp":avgconfig[1],
                      "FuncName":avgconfig[0],
                      "Updatadate":iotupdate}
            try:
                result=requests.post(url,listdata,timeout=10)
                if(result.status_code!=200):
                    print("数据上传异常")
                elif(result.status_code==200):
                    print("数据上传成功")
                    print("上传数据为\n=================\n%s"%listdata)
                    return  listdata
            except :
                print(requests.exceptions.RequestException(DHT.PostToServer))#捕获异常
                return requests.exceptions.RequestException
    #生成日志，包括异常日志以及正常日志
    # def ErrorLog(self,errlist):
    #     logFilePath="/var/log/iotTemperature_err.log"
    #     exceptionLogFile=open(logFilePath,"a+")
    #     exceptionLogFile.writelines(errlist)
    #     exceptionLogFile.close()
    def Log(self,contextlist,avgconfig):
        logFilePath=avgconfig[5]
        logFilePath=open(logFilePath,"a+")
        logFilePath.write("\n")
        logtime=datetime.datetime.now().strftime("%Y.%m.%d %H:%M:%S")
        logcontext =str(logtime)+str(contextlist)
        logFilePath.writelines(logcontext)
        logFilePath.close()
    def AppConfig(self):
        config=[]
        xmlFile = open("APP.config", "r")
        try:
            configFile=minidom.parse(xmlFile)
            rootNote=configFile.documentElement
            funcname=rootNote.getElementsByTagName("functionName")
            iotIp=rootNote.getElementsByTagName("IotDeviceIP")
            webAPI=rootNote.getElementsByTagName("WebInterface")
            dhtpin=rootNote.getElementsByTagName("Dht11Pin")
            dhtclass=rootNote.getElementsByTagName("DhtClass")
            logpath=rootNote.getElementsByTagName("LogPath")
            config.append(funcname[0].firstChild.data)
            config.append(iotIp[0].firstChild.data)
            config.append(webAPI[0].firstChild.data)
            config.append(dhtpin[0].firstChild.data)
            config.append(dhtclass[0].firstChild.data)
            config.append(logpath[0].firstChild.data)
            print(os.getcwd())
            return config
            #
            # print(funcname[0].firstChild.data)
            # print(iotIp[0].firstChild.data)
            # print(webAPI[0].firstChild.data)
            # print(dhtpin[0].firstChild.data)
            # print(dhtclass[0].firstChild.data)
            # print(logpath[0].firstChild.data)

            # for empone in emp:
            #     print(empone.nodeName)
            #     print(empone.nodeValue)
            # print(emp)

        except BaseException as Bex:
            print(os.getcwd())
            print(Bex)
        finally:
            xmlFile.close()


if __name__=="__main__": #这里是主运行程序,但是没有设置定时运行
    # while(True):
    dht=DHT()
    loginfo_normal=[]
    config=dht.AppConfig()
    iotdata=dht.ReadDht(config)
    postdata=dht.PostToServer(iotdata[1],iotdata[0],config)
    loginfo_normal.append(iotdata)
    loginfo_normal.append(postdata)
    print(loginfo_normal)
    dht.Log(loginfo_normal,config)


    # dht.AddDataToSQL(dht.Humidity,dht.Temper)

