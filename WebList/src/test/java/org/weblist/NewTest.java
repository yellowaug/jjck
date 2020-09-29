package org.weblist;

import org.TimeAct.ITimeSpan;
import org.junit.Test;
import org.newWebList.IWebListServer;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;
import org.testpack.ISomeServer;


public class NewTest {
    @Test
    public void mytest1() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("testconfig/tconfig.xml");
        ISomeServer ss= (ISomeServer) ac.getBean("someServer");
//        System.out.println(ss.tDoSome(10));
        ss.tDoSome(10);
    }

    @Test
    public void mytest2() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("newwebConfig/newWebListConfig.xml");
        IWebListServer webList= (IWebListServer) ac.getBean("listServer");
        webList.ListApi("https://gcapi.muniuma.cn/api/Account/Authenticate","18978849681");
    }

    @Test
    public void mytest3() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("newwebConfig/newWebListConfig.xml");
        ITimeSpan timeSpan= (ITimeSpan) ac.getBean("timeSpan");
        timeSpan.getNowTime();
    }
}
