# 🔍 Hidden Object Game Prototype

Прототип гри в жанрі **Hidden Object** з акцентом на архітектурну чистоту, використання MVVM, патернів проектування та DI-контейнера **Zenject**.

## 📌 Особливості

- ✅ **MVVM**: логіка відділена від представлення
- ✅ **Zenject**: впровадження залежностей
- ✅ **UniRx**: реактивні зв'язки між ViewModel та View
- ✅ **Addressables**: динамічне завантаження ассетів
- ✅ **Object Pooling**: оптимізація інстансування об'єктів
- ✅ **State Machine**: керування ігровими станами (Loading, Menu, Playing, Restart)
- ✅ **Strategy Pattern**: модульна поведінка підказок
- ✅ **DOTween**: анімації підказок та взаємодій

---

## 🚀 Запуск

1. Встанови Unity **6000.0.25f**
2. Клонуй репозиторій
3. Увімкни **Addressables**: `Window → Asset Management → Addressables`
4. Встанови:
   - [Zenject](https://github.com/modesttree/Zenject)
   - [UniRx](https://github.com/neuecc/UniRx)
   - [DOTween](http://dotween.demigiant.com/)
5. Відкрий сцену `MainScene`  
6. Натисни **Play**

---

## 🧪 Основні патерни

- **MVVM**: UI прив’язаний до `ViewModel` через UniRx
- **Strategy**: підказки реалізовані як змінні стратегії (`IHintStrategy`)
- **State Machine**: кожен стан гри відокремлений класом (Loading, Menu, Playing)
- **Object Pool**: `HiddenObjectSpawner` використовує пул для генерації
- **Dependency Injection**: усі сервіси та стратегії ін'єктуються через Zenject

---

## 🖼 UI

- **Canvas/UI**: для кожного стану (`LoadingUI`, `MenuUI`, `PlayingUI`)
- **Анімації**: DOTween для підсвітки, натискань, зворотного зв’язку

---

## 📦 TODO

- [ ] Додати систему очок
- [ ] Реалізувати таймер/ліміт часу
- [ ] Адаптивний UI для мобільних
- [ ] Вибір рівня складності
