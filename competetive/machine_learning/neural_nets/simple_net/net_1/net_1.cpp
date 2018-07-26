//Solution 1: without hidden layer
#include <iostream>
#include <vector>

using namespace std;

class Neuron{
public:
    float value;
    vector<Neuron> inputs;
    vector<float> weights;

    virtual float transfer_function(){
    	float result = 0;
    	if (weights.size() == 0){
    		for (size_t i = 0; i < inputs.size(); i++)
        		result += inputs[i].predict();
		}
		else
        	for (size_t i = 0; i < inputs.size(); i++)
        		result += inputs[i].value * weights[i];
        value = result;
        return result;
    }
    virtual float predict(){
        return transfer_function() >= 0.5 ? 1 : 0;
    }
    
    Neuron(float val){
        value = val;
    }
    Neuron(vector<Neuron> input, vector<float> weight){
        inputs = input;
        weights = weight;
        transfer_function();
    }
    Neuron(){}
};

int main(){
    float val_vodka, val_rain, val_friend;
    cin >> val_vodka >> val_rain >> val_friend;
    Neuron vodka(val_vodka), rain(val_rain), best_friend(val_friend);
    
    vector<Neuron> values = {vodka, rain, best_friend};
    vector<float> weights = {0.5, -0.5, 0.5};

    Neuron result(values, weights);

    cout << result.predict() << endl;
    return 0;
}
