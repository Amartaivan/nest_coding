#include <iostream>
#include <string>

using namespace std;

int main(){
	
	string input1, input2;
	cin >> input1 >>input2;
	
	long result1 = 1, result2 = 1;
	
	for (char a : input1)
		result1 *= static_cast<long>(a - 'A' + 1); // 'A' - 'A' = 0 >> 'A' - 'A' + 1 = 1
	result1 %= 47;
	
	for (char a : input2)
		result2 *= static_cast<long>(a - 'A' + 1);
	result2 %= 47;
	
	cout << (result1 == result2 ? "GO" : "STAY");
	return 0;
}
