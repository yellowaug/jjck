package org.SmsAct;

import com.tencentcloudapi.common.Credential;
import com.tencentcloudapi.common.exception.TencentCloudSDKException;
import com.tencentcloudapi.sms.v20190711.SmsClient;
import com.tencentcloudapi.sms.v20190711.models.SendSmsRequest;
import com.tencentcloudapi.sms.v20190711.models.SendSmsResponse;
import org.Result.ListResult;
import org.aspectj.lang.ProceedingJoinPoint;
//import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.AfterReturning;
import org.aspectj.lang.annotation.Aspect;


@Aspect
public class SmsServer {
    @AfterReturning(value = "execution(* *..WebListServer.ListApi(..))",returning = "lsr")
    public void SendSmsAct(ListResult lsr){
        if (lsr.getWebCode()!=200){
            System.out.println(lsr.getUrl());
            System.out.println(lsr.getResultContent());
            System.out.println("发送短信方法");
            try {
                String secretId="AKIDmfHsEJXXGiwmLeGUFjwsYx5B4C4fPx7s";
                String secretKey="qQj12pRt4ybxlmY7VP9dqeBRBmBLIzsc";
                Credential cred=new Credential(secretId,secretKey);
                SmsClient client=new SmsClient(cred,"");
                SendSmsRequest req=new SendSmsRequest();
                req.setSmsSdkAppid("1400231254");
                req.setTemplateID("729851");
                String sign="木牛马";
                req.setSign(sign);
                String[] phoneNumbers={"+8618978849681","+8619163899701"};
                req.setPhoneNumberSet(phoneNumbers);
                String[] templateParams = {lsr.getUrl()};
                req.setTemplateParamSet(templateParams);
                SendSmsResponse res=client.SendSms(req);
                System.out.println(SendSmsResponse.toJsonString(res));
                System.out.println(res.getRequestId());
            } catch (TencentCloudSDKException e) {
                e.printStackTrace();
            }
        }
        else {
            System.out.println(lsr.getWebCode());
            System.out.println(lsr.getUrl());
            System.out.println(lsr.getResultContent());
        }
    }
}
