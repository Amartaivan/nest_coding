# C++ (Анхан шат)
### Анхны программ
Программ болгонд main гэсэн нэртэй функц зарласан байх хэрэгтэй.
Программистуудын уламжлал: аливаа хэл сурж эхлэхдээ хамгийн эхний программ нь "Hello, world!" гэж хэвлэдэг.
```c++
#include <iostream> //Энд iostream гэсэн нэртэй "толгой" файл нэмж байгаа. Энэ файлд оролт, гаралтын фукцууд болон классууд байдаг.

using namespace std;
/* 	c++ хэлний стандарт фукцууд энэ namespace-д байдаг.
	Оролт болон гаралт хийх бүртээ std:: гэж бичихгүйн тулд ингэж бичдэг. 
	Жишээ:
		std::cout << "Hello, world" << std::endl;
		std::cin >> n;
*/

int main(){ //Энэ бол программ гол функц. Програм нээгдэх бүртээ энэ функцээс эхэлнэ.

	cout << "Hello, world" << endl;
	return 0;   //Функц бүр утга буцаах ёстой. Тэр утгын төрлийг тухайн фукцийг зарлахдаа бичдэг. 
                //Жишээ: int буюу бүхэл тоо, void нь функц ямар ч утга буцаахгүйг илэрхийлдэг.
}
```
### Хувьсагчууд
Программ дотор мэдээлэл хадгалах аргуудын нэг нь бол хувьсагч. Үүнийг ашиглахын тулд эхлээд зарлах хэрэгтэй.
Зарлахдаа хувьсагчийн төрлийг компьютерт "заана". Жишээ:
* Тоо (int, float, double, long)
* Үсэг (char, wchar_t, гэх мэт...)
Бух хувьсагчууд signed (тэмдэгтэй, сөрөг ба эерэг байж болно), unsigned (тэмдэггүй, зөвхөн эерэг) байж болно.
Төрөл бүрийн хэмжээ өөр өөр байдаг. [Дэлгэрэнгүй](https://en.wikipedia.org/wiki/C_data_types)
```c++
bool a; 		//bool нь 2 утгатай: үнэн (true) ба худал (false)

short b; 		//−32767-аас +32767 хүртэл бүхэл тоо
int c;			//-2147483648-аас +2147483647 хүртэл бүхэл тоо
long d;
long long e;	//long ба long long нь int-т багтахгүй тоог хадгалахад хэрэглэгдэг

float f;
double g;		//float ба double-д бутархай тоо хадгална

char h;			//ASCII үсэг
wchar_t i;		//Үсэг (илүү их утгатай, кирилл үсэг, ханз, гэх мэт...)
```
### Арифметик үйлдэл
C++-т дараах төрлийн арифметик үйлдлүүд байдаг.
* Нэмэх (+)
* Хасах (-)
* Хуваах (/)
```c++
int x = 64 / 2; //32
```
* үржүүлэх (*)
```c++
int y = 2 * 53; //106
```
* Хуваахын үлдэгдлийг гаргах (% буюу modulus)
```c++
int y = 64;
cout << y % 56 << endl; // 8
```
### if оператор, нөхцөл
if оператор нь нөхцөл шалгахад хэрэглэдэг.
Блок схем дээр тэмдэглэвэл:
![if нөхцөл](http://2.bp.blogspot.com/-YimkS2x7vyA/T3tLwSL3TYI/AAAAAAAAAGY/9Wct8reM2VU/s1600/if.jpg "if нөхцөл")

Хэрвээ нөхцөл биелэж байвал (жишээ: блок схемын "Усл.1") эсвэл биелэхгүй байгаа бол тус тус өөр үйлдэл хийж болно ("Действие 1").
C++ жишээ:
```c++
if (x * 5 == 65) //Хэрвээ x*5 65-тай тэнцүү бол
    cout << "x = " << x << endl; //x хувьсагчаа хэвлэх
```
### switch оператор
switch оператор нь олон if бичихгүйн тулд хэрэглэгддэг.
```c++
switch (n){
	case 1:
		cout << "January" << endl;
		break;
	case 2:
		cout << "February" << endl;
		break;
	//Гэх мэт...
}
```
### Давталтууд
C++ хэлд олон янзын давталт байдаг. Жишээлбэл: for, while, do ... while, label-тай давталт зэрэг.
Блок схем дээр:

![давталт](https://c.mql4.com/book/2008/04/42.png "давталт")

Давталт нь нөхцөл биелэж байдгаа үед л ажилдаг.
Жишээ:
```c++
//1-ээс 5 хүртэл тоонуудын нийлбэрийг олох
int sum = 0; //Энд нийлбэрийг хадгална
for (int i = 0; /* Тоолуур хувьсагчаа зарласан */ i < 5; /* i 5-аас бага байгаа бол */ i++; /* i-аа нэгээр нэмэх */){
	sum += i; //sum дээр i-г нэмэх (sum = sum + i; гэсэнтэй адилхан)
}
cout << sum << endl; //Нийлбэрээ хэвлэх
```
### Массив
Массив нь санах ойд дараалан хэд хэд утга хадгалах үүрэгтэй. Жишээ:
```c++
int A[10] = {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}; //Массивыг зарласан нь
/*	Массивын аливаа элементэд хандахдаа индекс (массив дахь дэс дугаар) хэрэгтэй.
	Жишээ:
		cout << A[0] << endl; //Массивыг ихэнхдээ 0-ээс эхлэн дугаарладаг
		cout << A[1] << endl;
		cout << A[9] << endl;
	Гаралт:
		1
		2
		10
*/
```
Давталтыг массив руу хандахын тулд ашиглаж болно.
```c++
int n; //Массивын хэмжээ
cin >> n; //n-ыг унших

int A[n]; //n хэмжээтэй массив авлаа
for (int i = 0; i < n; i++){	//Энэ давталтанд бид массиваа дүүргэж байна
	cin >> A[n];
}
```
### Оролт ба гаралт
```c++
int n, m;
cin >> n >> m; //n ба m-ыг нэг зэрэг уншиж байна

cout << n * m << /* endl буюу end line шинэ мөр эхэлснийг илэрхийлдэг */ endl << n + m; 
//n ба m-ын үржвэрийг гаргах, шинэ мөрнөөс нийлбэрийг хэвлэх
``` 
Оролт:
```
2 4
```
Гаралт:
```
8
6
```