package org.weblist;

import com.tencentcloudapi.common.Credential;
import com.tencentcloudapi.common.exception.TencentCloudSDKException;
import com.tencentcloudapi.sms.v20190711.SmsClient;
import com.tencentcloudapi.sms.v20190711.models.SendSmsRequest;
import com.tencentcloudapi.sms.v20190711.models.SendSmsResponse;
import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;

@Aspect
public class Sms {
    @Around(value = "execution(* *..ListWebSite.listGcStiePost(..))")
    public Object sendSms(ProceedingJoinPoint pjp) {
//        try {
//            String secretId="AKIDmfHsEJXXGiwmLeGUFjwsYx5B4C4fPx7s";
//            String secretKey="qQj12pRt4ybxlmY7VP9dqeBRBmBLIzsc";
//            Credential cred = new Credential(secretId, secretKey);
//            SmsClient client = new SmsClient(cred, "ap-guangzhou");
//            SendSmsRequest req=new SendSmsRequest();
//            req.setSmsSdkAppid("1255356748");
//            req.setTemplateID("729851");
//            String[] phoneNumbers={"19163899701"};
//            req.setPhoneNumberSet(phoneNumbers);
//            String[] templateParams = {hostUrl};
//            req.setTemplateParamSet(templateParams);
//            SendSmsResponse res = client.SendSms(req);
//            System.out.println(SendSmsResponse.toJsonString(res));
//            System.out.println(res.getRequestId());
//        }
//        catch (TencentCloudSDKException e){
//            e.printStackTrace();
//        }
//        try {
//            pjp.proceed();
//            System.out.println("发送短信");
//        } catch (Throwable throwable) {
//            throwable.printStackTrace();
//        }
        return null;
    }

}

