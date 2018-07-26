/*
 * Зарим функцуудад олон хувьсагч дамжуулж болно. Жишээ: C-гийн printf функц
 * Энд print функц олон хувьсвагч авч, тэдгээрийг хэвлэдэг
*/

#include <iostream>

using namespace std;

template <typename T>
void print(T arg){
    cout << arg << endl;
}
template <typename T, typename... Ts> //Variadic template
void print(const T& arg, const Ts&... next){
    print(arg);
    print(next...);
}

int main(){

    print(1, 2, -1, 3, 4);
    return 0;
}