#pragma strict
#pragma implicit
#pragma downcast

var characters : TeamStart[];		// Finds all of the TeamStart Scripts that have characters in children.// Use this to see if we have any open slots.
var objectives : Transform[];		// Find all of the important items that need to be destroyed before continuing;

function Start () {
	var counter : int;
	var teamStartArray = GetComponentsInChildren(TeamStart);		// Get all of the start points that we are in control off (Children).
	for (i=0; i<teamStartArray.length; i++) {						// Tell the StartUps to see turn on.
		counter += (teamStartArray[i].CompareTag("Character") ? 1 : 0);
	}
	characters = new TeamStart[counter];
	counter = 0;													// This is so we can reference the array inside of the for loop.
	for (i=0; i<teamStartArray.length; i++) {						// Go through all StartUps and add the ones we need to the array.
		if (teamStartArray[i].CompareTag("Character")) {			// If this StartUp is in controll of a character.	
			characters[counter] = teamStartArray[i];					// Total teams equals total good StartUps in level.
			counter++;
		}
	}
	counter = 0;
	var find = GetComponentsInChildren(Transform);
	for (i=0; i<find.length; i++) {					// This is where we go through the StartUps to see how many are the ones we need. 
		counter += (find[i].CompareTag("Objective") ? 1 : 0);
	}	
	objectives = new Transform[counter];
	counter = 0;
	for (i=0; i<find.length; i++) {						// Go through all StartUps and add the ones we need to the array.
		if (find[i].CompareTag("Objective")) {			// Add to list only if this StartUp is a good one.
			objectives[counter] = find[i].transform;
			counter++;
		}
	}
}
