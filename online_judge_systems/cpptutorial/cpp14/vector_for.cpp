/*
 * vector болон std-ийн контейнерийт тусгай for давталт ашиглаж болно
*/

#include <iostream>
#include <vector>

using namespace std;

int min(vector<int> numbers){
    int result = 0x7FFFFFFF;

    for (int number : numbers) //numbers vector-с элементvvдийг авч number хувсагч руу хийж байна
        if (number < result)
            result = number;

    return result;
}
int max(vector<int> numbers){
    int result = -0x7FFFFFFF;

    for (int number : numbers)
        if (number > result)
            result = number;

    return result;
}

int main(){

    int n;
    cin >> n;

    vector<int> numbers(n);
    for (int i = 0; i < n; i++)
        cin >> numbers[i];

    cout << min(numbers) << endl;
    cout << max(numbers) << endl;
    return 0;
}