package org.newWebList;

import net.sf.json.JSONObject;
import org.Result.ListResult;
import org.apache.http.HttpStatus;
import org.apache.http.StatusLine;
import org.apache.http.client.methods.CloseableHttpResponse;
import org.apache.http.client.methods.HttpPost;
import org.apache.http.entity.StringEntity;
import org.apache.http.impl.client.CloseableHttpClient;
import org.apache.http.impl.client.HttpClients;
import org.springframework.context.ApplicationContext;
import org.springframework.context.support.ClassPathXmlApplicationContext;

import java.io.IOException;
import java.util.HashMap;
import java.util.Map;

public class WebListServer implements IWebListServer {
    @Override
    public ListResult ListApi(String apiUril,String account) {
        ApplicationContext ac=new ClassPathXmlApplicationContext("newwebConfig/ResultConfig.xml");
        ListResult result= (ListResult) ac.getBean("listResult");
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
            if (statu== HttpStatus.SC_OK ){
                result.setResultContent("接口调用正常");
                result.setWebCode(statu);
                result.setUrl(apiUril);
                System.out.println("网站状态正常，接口调用正常");

            }
            else {
                result.setResultContent("接口调用异常");
                result.setWebCode(statu);
                result.setUrl(apiUril);
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
        return result;
    }
}
