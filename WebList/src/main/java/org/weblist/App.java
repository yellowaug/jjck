package org.weblist;

import org.newWebList.IWebListServer;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Hello world!
 *
 */
public class App 
{
    public static void main( String[] args )
    {
//        ApplicationContext ac=new ClassPathXmlApplicationContext("configration.xml");
//        ListWebSite lsgc= (ListWebSite) ac.getBean("listgc");
//        ListWebSite lspt= (ListWebSite) ac.getBean("listpt");
//        ListWebSite lsjg= (ListWebSite) ac.getBean("listjg");
//        ListWebSite listGcApi= (ListWebSite) ac.getBean("listGcApi");
//        ListWebSite listPtApi= (ListWebSite) ac.getBean("listPtApi");
//        ListWebSite listJgApi= (ListWebSite) ac.getBean("listJgApi");
        ApplicationContext ac=new ClassPathXmlApplicationContext("newwebConfig/newWebListConfig.xml");
        IWebListServer webList= (IWebListServer) ac.getBean("listServer");
        webList.ListApi("https://gcapi.muniuma.cn/api/Account/Authenticate","18978849681");
        webList.ListApi("https://ptapi.muniuma.cn/api/Account/Authenticate","zengzhangquan");
        webList.ListApi("https://jgapi.muniuma.cn/api/Account/AuthenticateByPwd","18978849681");


    }
}
