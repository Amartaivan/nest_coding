#include <iostream>

using namespace std;

int main(){
	
	int rounds, i, j;
	cin >> rounds;
	if (rounds == 0)
		return 0;
	int A[rounds];
	
	A[0] = 1;
	cout << 1 << endl; //Round 1
	
	for (i = 1; i < rounds; i++)
		A[i] = 0;
	
	
	for (i = 2; i <= rounds; i++){
		
		cout << 1 << ' ';
		
		A[i - 1] = 1;
		
		for (j = i - 2; j > 0; j--){
			A[j] = A[j - 1] + A[j];
			cout << A[j] << ' ';
		}
		
		cout << 1 << endl;
	}
	return 0;
}
