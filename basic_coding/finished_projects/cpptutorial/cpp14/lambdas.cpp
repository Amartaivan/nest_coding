/*
 * Товчхон хэлбэл, lambda нь бол хувьсагчин доторх функц.
*/
#include <algorithm>
#include <iostream>
#include <vector>

using namespace std;

struct my_int{
    int value;
};

int main(){

    int n;
    cin >> n;

    vector<my_int> numbers(n);
    for (int i = 0; i < n; i++)
        cin >> numbers[i].value;

    sort(numbers.begin(), numbers.end(), 
    //lambda
    [](my_int a, my_int b) -> bool{ //[](дамжуулах хувьсачууд) -> (type: bool, int, float, double, г.м.) { код }
        return a.value < b.value;
    });

    for (my_int number : numbers)
        cout << number.value << ' ';
    cout << endl;
    return 0;
}