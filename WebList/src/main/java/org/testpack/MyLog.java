package org.testpack;

import org.aspectj.lang.annotation.AfterReturning;
import org.aspectj.lang.annotation.Aspect;

@Aspect
public class MyLog {
    @AfterReturning(value = "execution(* *..SomeServer.tDoSome(..))",returning = "res")
    public void addMyLog(Object res){
        System.out.println("后置方法"+res);
    }
}
