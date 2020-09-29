package org.weblist;

import static org.junit.Assert.assertTrue;

import org.junit.Test;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

/**
 * Unit test for simple App.
 */
public class AppTest 
{
    /**
     * Rigorous Test :-)
     */
    @Test
    public void shouldAnswerWithTrue()
    {
        assertTrue( true );
    }


    @Test
    public void testHttp() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("configration.xml");
        ListWebSite listWebSite= (ListWebSite) ac.getBean("listWeb");
        listWebSite.listGc();
    }

    @Test
    public void testListWebSite() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("configration.xml");
        ListWebSite listWebSite= (ListWebSite) ac.getBean("listWeb");

    }

    @Test
    public void testListWeb() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("configration.xml");
        ListWebSite lsgc= (ListWebSite) ac.getBean("listgc");
        ListWebSite lspt= (ListWebSite) ac.getBean("listpt");
        ListWebSite lsjg= (ListWebSite) ac.getBean("listjg");
        lsgc.listGcStiePost("https://gcapi.muniuma.cn/api/Account/Authenticate","18978849681");
        lspt.listGcStiePost("https://ptapi.muniuma.cn/api/Account/Authenticate","zengzhangquan");
        lsjg.listGcStiePost("https://jgapi.muniuma.cn/api/Account/AuthenticateByPwd","18978849681");
    }

    @Test
    public void testListApi() {
        ApplicationContext ac=new ClassPathXmlApplicationContext("configration.xml");
        ListWebSite lsgc= (ListWebSite) ac.getBean("listgc");
        ListWebSite lspt= (ListWebSite) ac.getBean("listpt");
        ListWebSite lsjg= (ListWebSite) ac.getBean("listjg");
        ListWebSite listGcApi= (ListWebSite) ac.getBean("listGcApi");
        ListWebSite listPtApi= (ListWebSite) ac.getBean("listPtApi");
        ListWebSite listJgApi= (ListWebSite) ac.getBean("listJgApi");
    }
}
