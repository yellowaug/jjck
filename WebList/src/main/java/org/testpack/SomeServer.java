package org.testpack;

public class SomeServer implements ISomeServer {
    @Override
    public void tDoSome(int s) {
        System.out.println("parm"+s);
//        return ""+s;
    }
}
