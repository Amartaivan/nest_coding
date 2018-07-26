#include <iostream>
#include <vector> //Arrays
#include <string>

using namespace std;

/*
struct test{
    int a;
    float b;
};
*/

int main(){

    //Variable declarations
    /*
    short b = 32000;
    long c = 143289439;
    long long d = 43920843284;
    float e = 2.0;
    double f = 2.4390284239;
    */
    //long float, long double
    //float fdsjioewhtwe = 9.1; //must be readable, not like this
    /*
    cout << result << endl;
    error: result not declared
    */

    //short, int, long, long long
    //string my_phone, my_address, my_info;    
    // while (true){
    //     cout << "My name is " << my_name << endl;
    //     cout << "What is your name?"<< endl;
    //     string name;
    //     cin >> name;
    //     cout << "Nice to meet you, " << name << "!" << endl << endl;
    //     my_name = name;
    // }
    /*
    while (true){
        cout << "What is your phone number? ";
        getline(cin, my_phone);
        getline(cin, my_address);
        my_info = my_phone + ' ' + my_address;
        cout << my_info << endl;
    }
    */

    // int n;
    // cin >> n;
    // bool isOdd; //0 is false, 1 is true
    // isOdd = n % 2 == 0;

    // while (true){
    //     char my_char;
    //     cin >> my_char;
    //     char my_char_converted = my_char - ('a' - 'A');

    //     cout << my_char_converted << endl;
    // }

    // int n;
    // cin >> n;
    // vector<int> test(n); //Equal to int test[n];

    // for (int i = 0; i < n; i++)
    //     cin >> test[i];

    // for (int number : test)
    //     cout << number << ' ';
    // cout << endl;

   string name;

   cin >> name;

   int name_length = name.length();

   for (int i = 0; i < name_length; i++){
       if (name[i] > 'Z'){
           name[i] = name[i] - 32;
       }
   }

   cout << name << endl;

    return 0;
}