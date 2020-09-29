package org.TimeAct;

import org.aspectj.lang.annotation.Aspect;
import org.aspectj.lang.annotation.Before;

import javax.xml.crypto.Data;
import java.text.SimpleDateFormat;
import java.util.Date;
@Aspect
public class TimeSpan implements ITimeSpan {

    @Override
    @Before("execution(* *..WebListServer.ListApi(..))")
    public void getNowTime() {
        Date date=new Date();
        System.out.println("==========================================================");
        SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd hh:mm:ss");
        String dateNowStr = sdf.format(date);
        System.out.println("运行时间：" + dateNowStr);
    }
}
