public class Builder : System.Collections.IEnumerator {
    private System.Int32 goal;
    private System.Int32 position;
    private UnityEngine.GameObject original;
    private UnityEngine.GameObject spawn;
    public Builder (UnityEngine.GameObject original, System.Int32 goal) {
        this.goal = goal;
        this.position = -1;
        this.original = original;
        this.spawn = original;
    }
    public System.Boolean MoveNext () {
        this.position += 1;
        System.Boolean next = this.position < this.goal;
        if (next) {
            UnityEngine.Transform parent = this.original.transform;
            UnityEngine.GameObject instantiate = Instantiate(this.original, this.spawn.transform);
            instantiate.name = System.String.Format("Spawn {0}", this.position);
            UnityEngine.Transform item = instantiate.transform;
            item.Translate (parent.localPosition);
            item.Rotate (parent.localRotation.eulerAngles);
			this.spawn = instantiate;
        }
        return next;
    }
    public void Reset () {
        this.goal = 0;
        this.position = -1;
    }
    System.Object System.Collections.IEnumerator.Current {
        get {
            return Current;
        }
    }
    UnityEngine.Object Current {
        get {
            try {
				return this.spawn;
            }
            catch (System.IndexOutOfRangeException) {
                throw new System.InvalidOperationException();
            }
        }
    }
    private UnityEngine.GameObject Instantiate (UnityEngine.GameObject original, UnityEngine.Transform parent) {
        return UnityEngine.GameObject.Instantiate(
            original,
            parent.position,
            parent.rotation,
            parent.parent
        );
    }
	public void stark (UnityEngine.GameObject original, UnityEngine.GameObject spawn, System.Int32 remain) {
		UnityEngine.Transform parent = original.transform;
		UnityEngine.GameObject instantiate = Instantiate(original, spawn.transform);
		instantiate.name = System.String.Format("Spawn {0}", remain);

		UnityEngine.Transform item = instantiate.transform;
		//item.parent = parent.parent;
		item.Translate (parent.localPosition);
		item.Rotate (parent.localRotation.eulerAngles);
		//item.AddComponent<VRCSDK2.VRC_EventHandler>();
		//item.AddComponent<VRCSDK2.VRC_Trigger>();
		//VRCSDK2.VRC_Trigger trigger = item.GetComponent<VRCSDK2.VRC_Trigger>();
		//trigger.Triggers = AddTriggers();
		stark (original, instantiate, remain);
	}
}