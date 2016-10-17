# TeamZenmaiSiki


## 命名規則  
・変数  
ローワーキャメルケース

・定数  
ビッグスネーク  

・クラス、構造体、関数  
キャメルケース

・それ以外  
ローワーキャメルケース



以下サンプルソース
~~~
void SampleProgram()
{
 enum SampleEnum
 {
  sample1 = 0,
  sample2
 }

 struct SampleStruct
 {
  int   sampleInt;
  float sampleFloat;
 }

 const int CONSTANT_VARIABLE = 10;

 int temp;

 void SampleFunction(int sample_)
 {
   temp = sample_;
 }
}
~~~
