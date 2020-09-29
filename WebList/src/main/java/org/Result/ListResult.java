package org.Result;

public class ListResult {
    private String resultContent;
    private int webCode;
    private String url;
    public void setResultContent(String resultContent) {
        this.resultContent = resultContent;
    }

    public void setWebCode(int webCode) {
        this.webCode = webCode;
    }

    public String getResultContent() {
        return resultContent;
    }

    public int getWebCode() {
        return webCode;
    }

    public void setUrl(String url) {
        this.url = url;
    }

    public String getUrl() {
        return url;
    }

    @Override
    public String toString() {
        return "ListResult{" +
                "resultContent='" + resultContent + '\'' +
                ", webCode=" + webCode +'\'' +
                ", url="+url+
                '}';
    }
}
