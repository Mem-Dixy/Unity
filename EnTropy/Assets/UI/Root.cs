using UnityEngine.UIElements;
namespace EnTropy {
	public class Root<Type> where Type : VisualElement {
		public static Type Q(VisualElement root, System.String className) {
			return root.Q<Type>(className);
		}
		public static UQueryBuilder<Type> Query(VisualElement root, System.String className) {
			return root.Query<Type>(className);
		}
	}
}