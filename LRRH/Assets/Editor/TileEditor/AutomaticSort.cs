using UnityEditor;
using UnityEngine;

class LookAtMainCamera : ScriptableObject {

	[MenuItem ("Level Editor/Sort Enemy And Objects")]
	static void sortEnemyAndObjects () {
		//On récupère l'ensemble des mushrooms sur la map
		//Pour chacun, on détecte leur zone d'appartenance.
		var mushroomList =  GameObject.FindObjectsOfType<Mushroom>();
		foreach (Mushroom mushroom in mushroomList) {
			var screen = getZone (mushroom.transform);
			if (screen != null) {
				mushroom.transform.SetParent (screen.transform);
			}
		}

		var boarList =  GameObject.FindObjectsOfType<Boar>();
		foreach (Boar boar in boarList) {
			var screen = getZone (boar.transform);
			if (screen != null) {
				boar.transform.SetParent (screen.transform);
			}
		}

		var batList =  GameObject.FindObjectsOfType<Bat>();
		foreach (Bat bat in batList) {
			var screen = getZone (bat.transform);
			if (screen != null) {
				bat.transform.SetParent (screen.transform);
			}
		}

		var plantList =  GameObject.FindObjectsOfType<PlantScript>();
		foreach (PlantScript plant in plantList) {
			var screen = getZone (plant.transform);
			if (screen != null) {
				plant.transform.SetParent (screen.transform);
			}
		}

		var dogList =  GameObject.FindObjectsOfType<FuriousDog>();
		foreach (FuriousDog dog in dogList) {
			var screen = getZone (dog.transform);
			if (screen != null) {
				dog.transform.parent.SetParent (screen.transform);
			}
		}

		var bushList =  GameObject.FindObjectsOfType<BushScript>();
		foreach (BushScript bush in bushList) {
			var screen = getZone (bush.transform);
			if (screen != null) {
				bush.transform.SetParent (screen.transform);
			}
		}

		var levierList =  GameObject.FindObjectsOfType<Levier>();
		foreach (Levier levier in levierList) {
			var screen = getZone (levier.transform);
			if (screen != null) {
				levier.transform.SetParent (screen.transform);
			}
		}

		var savePointList =  GameObject.FindObjectsOfType<savePoint>();
		foreach (savePoint savePoint in savePointList) {
			var screen = getZone (savePoint.transform);
			if (screen != null) {
				savePoint.transform.SetParent (screen.transform);
			}
		}

	}

	static Screen getZone(Transform obj){
		var map = GameObject.FindObjectOfType<WorldMap> ();
		var screenList = map.GetComponentsInChildren<Screen> ();
		foreach (Screen screen in screenList) {
			if (obj.transform.position.x >= screen.minBound.x * 30 + 15 && obj.transform.position.y <= screen.minBound.y * 30 + 15
				&& obj.transform.position.x <= screen.maxBound.x * 30 - 15 && obj.transform.position.y >= screen.maxBound.y * 30 - 15) {
				return screen;
			}
		}
		return null;
	}

}