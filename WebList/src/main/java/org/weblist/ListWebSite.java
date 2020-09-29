package org.weblist;


import net.sf.json.JSONObject;
import netscape.javascript.JSObject;
import org.apache.http.HttpEntity;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.ClientProtocolException;
import org.apache.http.client.HttpClient;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpGet;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class ListWebSite {
    public void listGc(){
        CloseableHttpClient client = HttpClients.createDefault();
        ApplicationContext ac=new ClassPathXmlApplicationContext("configration.xml");
        HttpGet webGet= (HttpGet) ac.getBean("httpGet");

        CloseableHttpResponse response = null;
        try {
            response=client.execute(webGet);
            System.out.println(response.getStatusLine().getStatusCode());

        }
        catch (IOException e){
            e.printStackTrace();
        }
        finally {
            try {
                response.close();
                client.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
    public void listWebSite(String uri){
        CloseableHttpClient client= HttpClients.createDefault();
        HttpGet httpGet=new HttpGet(uri);
        CloseableHttpResponse response=null;
        try {
            response=client.execute(httpGet);
            int siteCode=response.getStatusLine().getStatusCode();
            System.out.println("相应状态码:"+siteCode);
            if (siteCode== HttpStatus.SC_OK){
                System.out.println("网站正常");
            }
            else {
                System.out.println("网站异常");
            }

        } catch (IOException e) {
            e.printStackTrace();
        }

    }
    public void listGcStiePost(String apiUril,String account){
        CloseableHttpClient client = HttpClients.createDefault();
        HttpPost authPost=new HttpPost(apiUril);
        authPost.setHeader("Content-Type", "application/json;charset=UTF-8");

        Map<String,String> userInfo= new HashMap<>();
        userInfo.put("factoryId","1");
        userInfo.put("password","Iybbh7dw7Fkwl4g0ztLfzQ==");
        userInfo.put("userName",account);
        //下面两个for循环没啥意义
//        for (String a:userInfo.keySet()){
//            System.out.println(a);
//        }
//
//        for (Map.Entry<String,String> user:userInfo.entrySet()){
//            System.out.println(user.getKey()+":"+user.getValue());
//
//        }
        JSONObject jsonObject=JSONObject.fromObject(userInfo);
        System.out.println(jsonObject.toString());
        StringEntity entity=new StringEntity(jsonObject.toString(),"UTF-8");
        authPost.setEntity(entity);
        CloseableHttpResponse response = null;
        try {
            response=client.execute(authPost);
            StatusLine statusLine=response.getStatusLine();
            int statu=statusLine.getStatusCode();
//            System.out.println(statu);
            if (statu==HttpStatus.SC_OK ){
                System.out.println("网站状态正常，接口调用正常");
            }
            else {
                System.out.println("网站异常");
                System.out.println(statu);
                System.out.println(apiUril);
            }

        }
        catch (IOException e){
            e.printStackTrace();
        }
        finally {
            try {
                response.close();
                client.close();
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
//        StringEntity stent=new StringEntity(userInfo.toString(),"UTF-8");
    }

    public ListWebSite() {
    }

    public ListWebSite(String uri) {
        this.listWebSite(uri);
    }

    public ListWebSite(String apiUri,String account){
        this.listGcStiePost(apiUri,account);
    }

}

